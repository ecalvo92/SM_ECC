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
    errorElement: "span"
  });

});