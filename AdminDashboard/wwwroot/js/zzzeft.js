
console.log("tttttest");
// upload image handle
let aly = document.querySelectorAll('.fileupload'); // load all imgs  tag input file
//let imagePaths = []; // will store all uploaded images paths;
//const imagePaths = Array.from({ length: 4 }).map(el => "");
aly.forEach((fileupload, index) => {
    fileupload.addEventListener('change', () => {
        console.log(fileupload); // print the input tag
        var element = fileupload;
        var formData = new FormData();
        var totalFiles = element.files.length;
        console.log(totalFiles);
        for (var i = 0; i < totalFiles; i++) {
            var file = element.files[i];
        }
        formData.append("file", file);
        console.log("here");
        $.ajax({
            type: 'POST',
            url: '/UploadImg/UploadLogo',
            //dataType:"json",  //contentType: "application/json; charset=utf-8",

            data: formData,
            contentType: false, // Not to set any content header  
            processData: false // Not to process data 
        })
            .done(function (response) {
                //console.log(response); 
                if (response.success) {
                    console.log("from done");
                    let label = document.querySelector(`label[for=${fileupload.id}]`);
                    label.style.backgroundImage = `url(/img/Uploads/${response.imagename})`;
                    $('#' + fileupload.id).removeAttr("type");
                    //$('#' + fileupload.id).css("cursor", "not-allowed");
                    //$('#' + fileupload.id).css("value", response.imagename);
                    //document.getElementById(fileupload.id).value = response.imagename;
                    document.getElementById(fileupload.id).value = `/img/Uploads/${response.imagename}`;
                    console.log("dddooonee");
                    let productImage = document.querySelector('.product-image');
                    //productImage.removeAttribute("background-image"); 
                    productImage.style.backgroundImage = `url(/img/Uploads/${response.imagename})`;

                    $(".product-image").css("background-size", "contain");
                    $(".product-image").css("border", "1px solid red");
                    $(".product-image").css("width", "200px");
                    $(".product-image").css("height", "200px");

                    console.log("FIFSH");
                }
            })
            .fail(function (XMLHttpRequest, textStatus, errorThrown) {
                alert("FAIL");
            });
    });

});

