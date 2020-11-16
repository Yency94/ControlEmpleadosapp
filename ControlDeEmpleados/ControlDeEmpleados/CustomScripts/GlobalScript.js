//Autor: Aurus Ortiz
//Funcion request GET asincrona
//Enviar url para request y id de elemento HTML a donde poner resultado
//callback funcion que llamar cuando termina la request
function requestAsync(url, id, callback) {
    var xmlhttp = new XMLHttpRequest();
    var div = document.getElementById(id);
    var contentActual;
    try {
        //$(div).addClass("hidden");
        //$(div).after('<i class="fa fa-cog fa-spin" style="font-size: 120px; color: #007dfa;"></i>');
        contentActual = div.innerHTML;
        div.innerHTML = '<i class="fa fa-cog fa-spin" style="font-size: 120px; color: #3c8dbc;"></i>';        
    } catch (err)
    {

    }

    xmlhttp.onreadystatechange = function () {
        if (xmlhttp.readyState == 4 && xmlhttp.status == 200) {
            try {
                div.innerHTML = xmlhttp.responseText;
            } catch (err) { }
            try {
                callback(xmlhttp);
            } catch (err) { }
        }
        else if(xmlhttp.readyState ==4) {
            try {
                div.innerHTML = contentActual;
                alert("La peticion al servidor fallo");
            } catch (err) { }
        }
    }
    xmlhttp.open("GET", url, true);
    xmlhttp.send();
};

//Autor: Aurus Ortiz
//Use esta funcion para pedir un dato al servidor, eg: un nombre, codigo, bool etc.
function pedirDato(url) {
    var req = new XMLHttpRequest();
    req.open("GET", url, false);
    req.send();
    
    return req.responseText;
};

//Autor: Aurus Ortiz
//funcion para pedir JSON al servidor, necesita la URL y una funcion callback a llamar
//tras completar la requests, la funcion callback debe recibir como parametro el objeto obtenido del servidor
function requestJSON(url,callback) {
    var xmlhttp = new XMLHttpRequest();

    xmlhttp.onreadystatechange = function () {
        if (xmlhttp.readyState == 4 && xmlhttp.status == 200) {
            var res =  JSON.parse(xmlhttp.responseText);
            try {
                callback(res);
            }
            catch (err) {
                console.log(err.message);
            }
        }
    }

    xmlhttp.open("GET", url, true);
    xmlhttp.send();
}

//Autor: Aurus Ortiz
//busca al asociado en el sisema por medio del codigo
//recibe el codigo a buscar y el id del input en donde poner el nombre del asociado encontrado
function obtenerAsociado(codigo, nombreField) {
    var divPadre = $('#' + nombreField).parent().parent();
    var obtenerInputCodigo = divPadre.children(":first").children().attr('id');
    if (codigo.length < 7) {

        var stringElementos = '0000000';
        var stringCodigo = codigo;

        var contadorCodigo = stringCodigo.length;
        var contadorElementos = stringElementos.length;
        var obtenerFaltantes = contadorElementos - contadorCodigo;
        var elementosAgregar = "";
        for (var i = 0; i < obtenerFaltantes; i++) {
            elementosAgregar += '0';
        }
        codigo = elementosAgregar + codigo;

        if (stringCodigo.length < 4) {
            document.getElementById(nombreField).value = "";
            return;
        }


        if (codigo.length == 0)
        {
            document.getElementById(nombreField).value = "";
            return;
        }
        if(obtenerInputCodigo == null)
        {
            obtenerInputCodigo = "";
        }
          
    }
        
    var url = "/frm_AsociadosConsultaGeneral/ObtenerAsociado/" + codigo;
    requestJSON(url, _construirCallback(nombreField, codigo, obtenerInputCodigo));
};

//Autor: -----
//Obtiene el código de préstamo del asociado
function obtenerCodPreAsociado(codigo, nombreField) {
    var divPadre = $('#' + nombreField).parent().parent();
    var obtenerInputCodigo = divPadre.children(":first").children().attr('id');
    if (codigo.length < 7) {

        var stringElementos = '0000000';
        var stringCodigo = codigo;

        var contadorCodigo = stringCodigo.length;
        var contadorElementos = stringElementos.length;
        var obtenerFaltantes = contadorElementos - contadorCodigo;
        var elementosAgregar = "";
        for (var i = 0; i < obtenerFaltantes; i++) {
            elementosAgregar += '0';
        }
        codigo = elementosAgregar + codigo;

        if (stringCodigo.length < 4) {
            document.getElementById(nombreField).value = "";
            return;
        }


        if (codigo.length == 0) {
            document.getElementById(nombreField).value = "";
            return;
        }
        if (obtenerInputCodigo == null) {
            obtenerInputCodigo = "";
        }

    }

    var url = "/frm_AsociadosConsultaGeneral/ObtenerCodPreAsociado/" + codigo;
    requestJSON(url, _construirCallbackN(nombreField, codigo, obtenerInputCodigo));
};

//Autor: Aurus Ortiz
//construimos la funcion callback a llamar cuando la request n obtenerAsociado() halla conluido
function _construirCallback(nombreField, codigo, obtenerInputCodigo) {

    return function (asociado) {
        if (asociado.length === 0) {
            document.getElementById(nombreField).value = "No Existe un asociado con éste código";
            return;
        }
        var nombre = asociado[0].nombre + " " + asociado[0].apellido;
        document.getElementById(nombreField).value = nombre;

        if(obtenerInputCodigo != "")        
        {
            $('#' + obtenerInputCodigo).val(codigo);
            //Opcion para fecha de certificados solamente
            var date = new Date();
            var day = date.getDate();        //  day
            var dia;
            if (day < 10) {

                dia = "0" + day;
            }
            else {
                dia = day;
            }
            var month = date.getMonth() + 1;    //  month
            var mes;
            if (month < 10) {

                mes = "0" + month;
            }
            else {
                mes = month;
            }
            var year = date.getFullYear();  //  year
            var hour = date.getHours();     //  hours 
            var hora;
            if (hour < 10) {

                hora = "0" + hour;
            }
            else {
                hora = hour;
            }
            var minute = date.getMinutes(); //  minutes
            var minuto;
            if (minute < 10) {

                minuto = "0" + minute;
            }
            else {
                minuto = minute;
            }
            var second = date.getSeconds();
            var segundo;
            if (second < 10) {

                segundo = "0" + second;
            }
            else {
                segundo = second;
            }
            dateOperacion = dia + "/" + mes + "/" + year + " " + hora + ':' + minuto + ':' + segundo;

            if ($('#p_fechaOperacion').length)         // use this if you are using id to check
            {
                $('#p_fechaOperacion').val(dateOperacion);
            }

            //Fin proceso hora certificado solamente

            //Proceso correlativo certificado solamente
            if ($('.CodCertificadoValCorrelativo').length)
            {
                if ($('.NombreCertificadoVal').length)
                {
                    var NombreCertificado = $('.NombreCertificadoVal').val();
                    if (NombreCertificado != "")
                    {
                        if (NombreCertificado != "No Existe un asociado con éste código")
                        {
                            obtenerCorrelativo();
                        }   
                    }   
                }                
            }
        }
        //var inputAsociado = document.getElementById('CODIGOASOC');
        //if (inputAsociado != null)
        //{
        //    document.getElementById('CODIGOASOC').value = codigo;

        //}
        //var inputAsociado = document.getElementById('CODIGOASO');
        //if (inputAsociado != null) {
        //    document.getElementById('CODIGOASOC').value = codigo;
           
        //}
    };

};


//Alex
//Este script se duplico del obtenerCodigoAsociado por motivos de que modificar el padre cause problemas
function obtenerAsociadoN(codigo, nombreField, codAsociado) {
    if (codigo.length < 7) {
        var stringElementos = '0000000';
        var stringCodigo = codigo;

        var contadorCodigo = stringCodigo.length;
        var contadorElementos = stringElementos.length;
        var obtenerFaltantes = contadorElementos - contadorCodigo;
        var elementosAgregar = "";
        for (var i = 0; i < obtenerFaltantes; i++) {
            elementosAgregar += '0';
        }
        codigo = elementosAgregar + codigo;

        if (stringCodigo.length < 4)
        {
            document.getElementById(nombreField).value = "";
            return;
        }


        if (codigo.length == 0)
        {
            document.getElementById(nombreField).value = "";
            return;
        }
            
        
    }

    var url = "/frm_AsociadosConsultaGeneral/ObtenerAsociado/" + codigo;
    requestJSON(url, _construirCallbackN(nombreField, codAsociado, codigo));
};

//Alex
//Este script se duplico del _construirCallback por motivos de que modificar el padre cause problemas
function _construirCallbackN(nombreField, codAsociado, codigo) {

    return function (asociado) {
        if (asociado.length === 0) {
            document.getElementById(nombreField).value = "No Existe un asociado con éste código";
            return;
        }
        var nombre = asociado[0].nombre + " " + asociado[0].apellido;

        document.getElementById(nombreField).value = nombre;
        document.getElementById(codAsociado).value = codigo;
    };

};


//calcula la edad en base a la fecha de nacimiento
//enviar fecha en forma: yyyy-MM-dd
function getAge(dateString) {
    dateString += " 00:00:00";
    var today = new Date();
    var birthDate = new Date(dateString);
    var age = today.getFullYear() - birthDate.getFullYear();
    var m = today.getMonth() - birthDate.getMonth();
    if (m < 0 || (m === 0 && today.getDate() < birthDate.getDate())) {
        age--;
    }
    return age;
}

//Devuevel la fecha del dia en formato YYYY-MM-dd
function getDate() {
    var today = new Date();
    var dd = today.getDate();
    var mm = today.getMonth() + 1; //January is 0!
    var yyyy = today.getFullYear();

    if (dd < 10) {
        dd = '0' + dd
    }

    if (mm < 10) {
        mm = '0' + mm
    }

    today = yyyy + '-' + mm + '-' + dd;
    return today;
}

function activarValidaciones() {
    //Registrar validaciones jquery para la form
    var $form = $("form");
    $form.removeData("validator");
    $form.removeData("unobtrusiveValidation");
    $.validator.unobtrusive.parse("form");
}

$.fn.scrollView = function () {
    return this.each(function () {
        $('html, body').animate({
            scrollTop: $(this).offset().top
        }, 1000);
    });
}

function maskDui(){
    $('.dui').inputmask("99999999-9");
    $('.nit').inputmask("9999-999999-999-9");
    
}

//Autor: Byron Chavarría
//Función utilizada para colocar una mascara personalizada a un input de tipo text, utilizando el id del control
function mask(control, mascara) {
    $('#' + control).inputmask({
        mask: mascara
    });
}

//Autor: Byron Chavarría
//Función utilizada para colocar una mascara personalizada a un input de tipo text, utilizando el name de control
function maskByName(name, mascara) {
    $('input[name='+ name + ']').inputmask({
        mask: mascara
    });
}

function _checkNames(){
    $(".form-control").each(function(){
       this.value = this.name; 
    });
}

//Autor: Stanley
//Para numeros con  punto decimal
function isNumberKey(evt) {
    var charCode = (evt.which) ? evt.which : event.keyCode;
    if (charCode == 46 && evt.srcElement.value.split('.').length > 1) { return false; }
    if (charCode != 46 && charCode > 31 && (charCode < 48 || charCode > 57)) { return false; }
    return true;
}
//Continuación de función anterior 
function NumeroDecimal(e) {
    if (!isNumberKey(e)) { e.preventDefault(); }
}

//Autor: Byron Chavarría - Función utilizada para obtener la fecha Actual y
//Setearla en uno o dos controles guíados por el Id
function setActualDate(control1, control2) {
    var f = new Date();
    var fecha = f.getFullYear() + "-" + padl((f.getMonth() + 1)) + "-" + padl(f.getDate());

    //console.log(fecha);
    
    $('#' + control1).val(fecha);
    $('#' + control2).val(fecha);


    function padl(pnumero) {
        mystr = pnumero;
        pad = '00'
        return (pad + mystr).slice(-pad.length)
    }
}

//Autor: Byron Chavarría - Función para restringir un textbox a que solo acepte numeros enteros (sin decimales)
//Si desea permitir que en un textbox sólo admita numeros agregar ésta funcion en el evento onkeypress de la siguiente manera:
//onkeypress="return isNumber(event)"
function isNumber(evt) {
    evt = (evt) ? evt : window.event;
    var charCode = (evt.which) ? evt.which : evt.keyCode;
    if (charCode > 31 && (charCode < 48 || charCode > 57)) {
        return false;
    }
    return true;
}
//Autor: Byron Chavarría - Función utilizada para mostrar un "alert personalizado"
//(Modal de Bootstrap en el que se coloca el mensaje ingresado como titulo del modal)
//Se utilizo el js bootbox propiedad de un tercero
function customAlert(mensaje) {
    bootbox.alert(mensaje);
}
//Autor: Byron Chavarría - Función utilizada validar fechas Reales
//Es decir que si el usuario ingresa por ejemplo 2016-02-31 la función
//devolverá False, en cambio una fecha Real devolverá True
function checkDate(date) {
    var fechaf = date.split("/");
    var day = fechaf[2];
    var month = fechaf[1];
    var year = fechaf[0];
    var date = new Date(year, month, '0');
    if ((day - 0) > (date.getDate() - 0)) {
        return false;
    }
    return true;
}
