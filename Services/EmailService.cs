using System.Net.Mail;
using System.Net;
using tickets.api.Models.Domain;
using tickets.api.Models.DTO.Ticket;

namespace tickets.api.Services
{
    public interface IEmailService
    {
        Task SendTicketNotificationAsync(Ticket ticket);
    }


    public class EmailService : IEmailService
    {
        private readonly IConfiguration _config;
        

        public EmailService(IConfiguration config)
        {
            _config = config;
        }

        public async Task SendTicketNotificationAsync(Ticket ticket)
        {
            var smtpClient = new SmtpClient(_config["Smtp:Host"])
            {
                Port = int.Parse(_config["Smtp:Port"]),
                Credentials = new NetworkCredential(_config["Smtp:Email"], _config["Smtp:Password"]),
                EnableSsl = true
            };

            string htmlBody = GetEmailBody(ticket);

            var mail = new MailMessage
            {
                From = new MailAddress(_config["Smtp:Email"]),
                Subject = $"Nuevo ticket asignado - {ticket.Folio}",
                Body = htmlBody,
                IsBodyHtml = true
            };

            mail.To.Add("josecarlosgarciadiaz@gmail.com"); // O lista dinámica de responsables

            await smtpClient.SendMailAsync(mail);
        }

        private string GetEmailBody(Ticket ticket)
        {
            // Puedes cargar el HTML desde un archivo, o usar una plantilla Razor si prefieres.
            return $@"
        <!DOCTYPE html>
<html lang=""es"">
<head>
  <meta charset=""UTF-8"">
  <title>Nuevo Ticket Asignado</title>
  <style>
    body {{
      font-family: ""Roboto"", Arial, sans-serif;
      background-color: #f4f5f7;
      color: #2c3e50;
      margin: 0;
      padding: 2rem;
    }}
    .container {{
      max-width: 600px;
      margin: 0 auto;
      background: #ffffff;
      border-radius: 8px;
      box-shadow: 0 2px 10px rgba(0,0,0,0.05);
      padding: 2rem;
    }}
    .header {{
      text-align: center;
      padding-bottom: 1rem;
      border-bottom: 1px solid #e0e0e0;
    }}
    .header h2 {{
      margin: 0;
      color: #3f51b5;
    }}
    .ticket-info {{
      margin-top: 1.5rem;
    }}
    .ticket-info p {{
      margin: 0.5rem 0;
      line-height: 1.6;
    }}
    .label {{
      font-weight: bold;
      color: #616161;
    }}
    .footer {{
      margin-top: 2rem;
      font-size: 0.9rem;
      color: #888;
      text-align: center;
    }}
    .button {{
      display: inline-block;
      margin-top: 1.5rem;
      background-color: #3f51b5;
      color: #ffffff;
      padding: 0.75rem 1.5rem;
      border-radius: 4px;
      text-decoration: none;
      font-weight: 500;
    }}
  </style>
</head>
<body>
  <div class=""container"">
    <div class=""header"">
      <h2>🔔 Nuevo ticket asignado</h2>
    </div>
    <div class=""ticket-info"">
      <p>Estimado equipo,</p>
      <p>Se ha generado un nuevo ticket que requiere su atención. A continuación, se presentan los detalles:</p>

      <p><span class=""label"">Número de Ticket:</span> {ticket.Folio}</p>
      <p><span class=""label"">Fecha de creación:</span> {ticket.FechaCreacion.ToString("dd/MM/yyyy HH:mm")}</p>
      <p><span class=""label"">Solicitante:</span> {ticket.UsuarioCreacion.Nombre} {ticket.UsuarioCreacion.Apellidos}</p>
      <p><span class=""label"">Ubicación/Área:</span> {ticket.Area.Nombre}</p>
      <p><span class=""label"">Prioridad:</span> {ticket.Prioridad.Nombre}</p>
      <p><span class=""label"">Descripcion:</span> {ticket.Descripcion}</p>
      <p><span class=""label"">Area especifica:</span> {ticket.AreaEspecifica}</p>
      <p><span class=""label"">Nombre:</span> {ticket.NombreContacto}</p>
      <p><span class=""label"">Telefono:</span> {ticket.TelefonoContacto}</p>
      <p><span class=""label"">Correo:</span> {ticket.CorreoContacto}</p>
      <p><span class=""label"">Afecha la operacion:</span> {ticket.AfectaOperacion}</p>
      <p><span class=""label"">Desde cuando se presenta:</span> {ticket.DesdeCuando.ToString("dd/MM/yyyy HH:mm")}</p>

      <a href="" class=""button"">Abrir ticket</a>
    </div>
    <div class=""footer"">
      Este mensaje fue generado automáticamente por el sistema de tickets {_config["Configuracion:Nombre"]}.<br>
      Para más información, contacta a: {_config["Configuracion:Correo"]}
    </div>
  </div>
</body>
</html>
";
        }
    }

}
