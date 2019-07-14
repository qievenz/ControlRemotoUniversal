// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

function actualizarteclas(invalue) {
    
    switch (invalue) {
        case "Samsung_UN32J5500":
            document.getElementById("FlowControl").hidden = true;
            document.getElementById("SamsungControl").hidden = false;
            break;
        case "Flow":
            document.getElementById("SamsungControl").hidden = true;
            document.getElementById("FlowControl").hidden = false;
            break;
    }
}

function PostBotonPWR(inid, invalue) {
    PostBoton(invalue);

    elemento = document.getElementById(inid);
    switch (invalue) {
        case "WW_RCH-3218ND AUTO_ON":
            elemento.className = "btn ACOFF"
            elemento.textContent = "AC OFF";
            elemento.value = "WW_RCH-3218ND AUTO_OFF";
            break;
        case "WW_RCH-3218ND AUTO_OFF":
            elemento.className = "btn ACON"
            elemento.textContent = "AC ON";
            elemento.value = "WW_RCH-3218ND AUTO_ON";
            break;
        /*
        case "Samsung_UN32J5500 KEY_POWER":
            (elemento.style.backgroundColor ==  rgb(116, 3, 3)) ? elemento.style.backgroundColor = "green" : elemento.style.backgroundColor = "red";
            break;
        case "Flow KEY_POWER":
            (elemento.style.backgroundColor ==  rgb(116, 3, 3)) ? elemento.style.backgroundColor = "green" : elemento.style.backgroundColor = "red";
            break;
        */
    }

}

function PostBoton(elemen) {
    console.log(elemen);
    if (elemen != ""){
        if(elemen.split(" ").length == 2){
            $.ajax({
                type: "POST",
                url: "Home/check?button=" + elemen,
                contentType: "application/json; charset=utf-8",
                dataType: "json"
            });
        }
    }
}