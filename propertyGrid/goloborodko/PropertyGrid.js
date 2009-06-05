// JScript File

    function categoryShow(nodeId,categoryId)
    {
        var node = document.getElementById(nodeId);
        var toHide = node.className != 'expand';
        node.className        = node.className=='expand'?'collapse':'expand';

        $('.' + categoryId).each(function() {
            if (toHide)
                $(this).hide('fast');
            else
                $(this).show('fast');
        });

    }
    function descriptionShow(title, text) {
        $("#description-text").html(text);
        $("#description-title").html(title);
    
    }
                            