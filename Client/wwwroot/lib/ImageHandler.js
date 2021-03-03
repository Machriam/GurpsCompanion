window.imageFunctions = {
    IMAGE_MIME_REGEX = /^image\/(p?jpeg|gif|png)$/i,
    loadImage = function (file) {
        var reader = new FileReader();
        reader.onload = function (e) {
            var img = document.createElement('img');
            img.src = e.target.result;

            var range = window.getSelection().getRangeAt(0);
            range.deleteContents();
            range.insertNode(img);
        };
        reader.readAsDataURL(file);
    },
    document.onpaste = function (e) {
        var items = e.clipboardData.items;

        for (var i = 0; i < items.length; i++) {
            if (IMAGE_MIME_REGEX.test(items[i].type)) {
                loadImage(items[i].getAsFile());
                return;
            }
        }

        e.PreventDefault()
    }
}