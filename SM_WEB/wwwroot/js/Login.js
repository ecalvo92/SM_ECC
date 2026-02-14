$(function () {

  $("#formLogin").validate({
    rules: {
      CorreoElectronico: {
        required: true,
        email: true
      },
      Contrasenna: {
        required: true
      }
    },
    messages: {
      CorreoElectronico: {
        required: "Campo obligatorio",
        email: "Formato incorrecto"
      },
      Contrasenna: {
        required: "Campo obligatorio"
      }
    },
    errorClass: "text-danger",
    errorElement: "span"
  });

});