class ImageHandler {
    pasteArea;
    pasteAreaContext;
    createImage(source) {
        let pastedImage = new Image();
        this.initializeCanvas();
        pastedImage.onload = function () {
            let instance = ImageHandler.getInstance();
            instance.pasteArea.width = pastedImage.width;
            instance.pasteArea.height = pastedImage.height;
            instance.pasteAreaContext.drawImage(pastedImage, 0, 0);
        };
        pastedImage.src = source;
    }
    initializeCanvas() {
        let pasteArea = document.getElementById("pasteArea");
        this.pasteArea = pasteArea;
        this.pasteAreaContext = pasteArea.getContext("2d");
    }
    pasteHandler(e) {
        if (e.clipboardData) {
            let instance = ImageHandler.getInstance();
            let items = e.clipboardData.items;
            if (!items) return;
            let isImage = false;
            for (let i = 0; i < items.length; i++) {
                if (items[i].type.indexOf("image") !== -1) {
                    let blob = items[i].getAsFile();
                    let urlObj = window.URL || window.webkitURL;
                    let source = urlObj.createObjectURL(blob);
                    instance.createImage(source);
                    isImage = true;
                }
            }
            if (isImage == true) {
                e.preventDefault();
            }
        }
    }
    getImageData() {
    }
    static getInstance() {
        return window.imageFunctions.instance;
    }
}
window.imageFunctions = {
    instance: new ImageHandler(),
    initialize: function () {
        let instance = ImageHandler.getInstance();
        instance.initializeCanvas();
        document.addEventListener("paste", instance.pasteHandler, false);
    },
    getImageData: function () {
        let instance = ImageHandler.getInstance();
        return instance.pasteArea.toDataURL();
    },
    setImageData: function (data) {
        let instance = ImageHandler.getInstance();
        instance.createImage(data);
    },
    dispose: function () {
        document.removeEventListener("paste", instance.pasteHandler, false);
    }
}