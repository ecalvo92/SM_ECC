$(function () {

  $("#formRegistro").validate({
    rules: {
      Identificacion: {
        required: true
      },
      Nombre: {
        required: true
      },
      CorreoElectronico: {
        required: true,
        email: true
      },
      Contrasenna: {
        required: true
      }
    },
    messages: {
      Identificacion: {
        required: "Campo obligatorio"
      },
      Nombre: {
        required: "Campo obligatorio"
      },
      CorreoElectronico: {
        required: "Campo obligatorio",
        email: "Formato incorrecto"
      },
      Contrasenna: {
        required: "Campo obligatorio"
      }
    },
    errorClass: "text-white",
    errorElement: "span",
    highlight: function (element) {
      $(element).addClass("is-invalid");
    },
    unhighlight: function (element) {
      $(element).removeClass("is-invalid");
    }
  });

});

function ConsultarNombre() {

  $("#Nombre").val("");
  let identificacion = $("#Identificacion").val();

  if (identificacion.length >= 9) {
    $.ajax({
      method: "GET",
      url: "https://apis.gometa.org/cedulas/" + identificacion,
      dataType: "json",
      success: function (response) {
        $("#Nombre").val(response.results[0].fullname);
      }
    });
  }

}