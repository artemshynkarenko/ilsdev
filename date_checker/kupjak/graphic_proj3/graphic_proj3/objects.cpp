#include "objects.h"

double aaa  = atan2(-20.0, -20.0);

//Object
void Object::add_line(Line & l){
	source.push_back(l.begin);
	source.push_back(l.end);
	dest.push_back(Vector4d());
	dest.push_back(Vector4d());
}
Object::Object(const Poligon & p, const int cp, const int cl ,const bool pp, const bool pl):color_point(cp), color_line(cl), drawed_point(pp), drawed_line(pl){
	int n = 2*p.vertex.size();
	source.resize(n);
	dest.resize(n);
	for (int i=0; i<(int)p.vertex.size()-1; ++i){
		source[2*i] = Point(p.vertex[i]);
		source[2*i+1] = Point(p.vertex[i+1]);
	}
	source[n-2] = Point(p.vertex[p.vertex.size()-1]);
	source[n-1] = Point(p.vertex[0]);

	Sphere_point sp = point_to_sphere_point(p.normal);
	Matrix44d a;
	a.rotate_y(sp.theta);
	a.rotate_z(sp.phi);
	a.position(p.position);
	calc(a);
	source = dest;
}

void Object::calc(const Matrix44d & matrix){
	//calculate product of points and matrix
	int n = size();
	for(int i = 0; i<n; ++i)
		for(int j = 0; j<4; ++j){
			dest[i][j] = 0;
			for(int k = 0; k<4; ++k)
				dest[i][j] += source[i][k]*matrix[k][j];
		}
	// norming result
	for(int i = 0; i<n; ++i)
		if (dest[i][3]!=0)
			for (int j = 0; j<4; ++j)
				dest[i][j] /= dest[i][3];
		else
			for (int j = 0; j<4; ++j)
				dest[i][j]/= eps;
}

void Object::marge(const Object & o){
	for (int i = 0; i<o.size(); ++i){
		source.push_back(o.source[i]);
		dest.push_back(o.dest[i]);
	}
}


//Objects
void Objects::calc_all(){
	for (int i = 0; i < (int)all.size(); ++i)
		all[i].calc(matrix);
}

void Objects::add_from_file(string file_name){
	ifstream ifs(file_name.c_str());
	this->add(Object());
	Poligon p;
	ifs >> p;
	all[all.size()-1] = p;
}


//Matrix44d

Point & operator*=(Point & p, const Matrix44d & m){
	Vector4d v(p);
	v *= m;
	if (v[3]!=0)
		for(int i=0; i<4; ++i)
			v[i]/=v[3];
	else
		for(int i=0; i<4; ++i)
			v[i]/=eps;
	p = Point(v[0], v[1], v[2]);
	return p;
}

Vector4d & operator*=(Vector4d & v, const Matrix44d & m){
	Vector4d res;
	for (int i=0; i<4; ++i)
		for(int j=0; j<4; ++j)
		res[i] += v[j]*m[j][i];
	v = res;
	return v;
}

void Matrix44d::rotate_x(double a){
	Matrix44d res;
	res[0] = Vector4d(1,  0     , 0      , 0);
	res[1] = Vector4d(0,  cos(a), sin(a) , 0);
	res[2] = Vector4d(0, -sin(a), cos(a) , 0);
	res[3] = Vector4d(0,  0     , 0      , 1);

	mult(res);
}

void Matrix44d::rotate_y(double b){
	Matrix44d res;
	res[0] = Vector4d(cos(b), 0, -sin(b), 0);
	res[1] = Vector4d(0     , 1,  0     , 0);
	res[2] = Vector4d(sin(b), 0,  cos(b), 0);
	res[3] = Vector4d(0     , 0,  0     , 1);

	mult(res);
}

void Matrix44d::rotate_z(double c){
	Matrix44d res;
	res[0] = Vector4d( cos(c), sin(c), 0,  0);
	res[1] = Vector4d(-sin(c), cos(c), 0,  0);
	res[2] = Vector4d( 0     , 0     , 1, 0);
	res[3] = Vector4d( 0     , 0     , 0, 1);
	mult(res);
}
void Matrix44d::position(const Point & p){
	Matrix44d res;
	res[0] = Vector4d(1 , 0, 0, 0);
	res[1] = Vector4d(0 , 1, 0, 0);
	res[2] = Vector4d(0 , 0, 1, 0);
	res[3] = Vector4d(p.x , p.y, p.z, 1);
	mult(res);
}

void Matrix44d::scale(double a){
	Matrix44d res;
	res[0] = Vector4d(a , 0, 0, 0);
	res[1] = Vector4d(0 , a, 0, 0);
	res[2] = Vector4d(0 , 0, a, 0);
	res[3] = Vector4d(0 , 0, 0, 1);
	mult(res);
}

void Matrix44d::rotate_vector_z(const Point & p){
	//rotate_vector_abc(point_to_rotation_point(p));
	Sphere_point sp = point_to_sphere_point(p);
	rotate_z(-sp.phi);
	rotate_y(-(pi/2-sp.theta));
}

void Matrix44d::rotate_vector_abc(const Rotation_point & rp){
	rotate_x(rp.a);
	rotate_y(rp.b);
	rotate_z(rp.c);
}

void Matrix44d::perspective(const Point & from, const Point & to){
	Rotation_point rp = point_to_rotation_point(from-to);
	Point p = from - to;
	Sphere_point sp = point_to_sphere_point(p);

	position(-to);
	rotate_z(-sp.phi);
	rotate_y(-(pi/2-sp.theta));


	Matrix44d res;
	res[0] = Vector4d(1 , 0, 0, -1/(from.x-to.x));
	res[1] = Vector4d(0 , 1, 0, 0);
	res[2] = Vector4d(0 , 0, 1, 0);
	res[3] = Vector4d(0 , 0, 0, 1);
	mult(res);
	

	//rotate_y((pi/2-sp.theta));
	//rotate_z(sp.phi);
	//position(to);
}



void Matrix44d::mult(const Matrix44d & a){
	Matrix44d b(*this);
	for(int i = 0; i<4; ++i)
		for(int j = 0; j<4; ++j){
			(*this)[i][j] = 0;
			for(int k = 0; k<4; ++k)
				(*this)[i][j] += b[i][k]*a[k][j];
		}
}

//Poligon
istream& operator>> (istream & is, Poligon & p){
	is >> p.position;
	is >> p.normal;
	int n;
	is >> n;
	p.vertex.resize(n);
	for(int i=0; i<n; ++i)
		is >> p.vertex[i];
	return is;
}

void Poligon::split(const int count){
	int n = vertex.size();
	if (n >= count)
		return;
	
	vertex.push_back(vertex[0]);
	vector<double> len(n, 0.0);
	for(int i = 0; i<n; ++i)
		len[i] = length2(vertex[i+1]-vertex[i]);
	vector<int> split_count(n, 0);

	for(int k = n; k < count; ++k){
		int ind = 0;
		double m_el = len[ind]/(split_count[ind]+1);
		for (int i = 1; i<n; ++i){
			double r = len[i]/(split_count[i]+1);
			if (r > m_el){
				r = m_el;
				ind = i;
			}
		}
		++split_count[ind];
	}
	
	vector<Point2> new_vertex;
	for(int i = 0; i<n; ++i){
		for(int j = 0; j< split_count[i]+1; ++j)
			new_vertex.push_back(vertex[i]+(vertex[i+1]-vertex[i])*((double)j/(split_count[i]+1)));
	}
	vertex = new_vertex;
}


Point Vector4d_to_Point(const Vector4d & v){
	return Point(v[0],v[1],v[2]);
}
