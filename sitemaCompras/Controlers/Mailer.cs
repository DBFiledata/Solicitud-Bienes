namespace sitemaCompras.Controlers
{
    using System;
    using System.IO;
    using System.Net;
    using System.Net.Mail;
    using System.Web;

    public partial class Mailer
    {
    	public void Notify(string emailSolicitante, int id, string SB, string Etapa, string Estatus, string nombreSolicitante, string nombreTecnico, string msj, int estado)
    	{
    		try
    		{
    			string sub = "";
    			string body = "";
    			MailAddress receiverEmail = new MailAddress(emailSolicitante, "Receiver");
    			string templatePath = HttpContext.Current.Server.MapPath("~/userscontent/modelEmail/email.html");
    			body = File.ReadAllText(templatePath);
    			if (estado == 1)
    			{
    				receiverEmail = new MailAddress(emailSolicitante, "Receiver");
    				sub = "Usted tiene un nuevo mensaje de su solicitud #" + id + ": ";
    				string URLCompras = "http://sistemas.asamblea.gob.pa:8000/compras/login";
    				body = body.Replace("[id]", id.ToString());
    				body = body.Replace("[SB]", SB.ToString());
    				body = body.Replace("[Etapa]", Etapa.ToString());
    				body = body.Replace("[Estatus]", Estatus.ToString());
    				body = body.Replace("[nombreSolicitante]", nombreSolicitante.ToString());
    				body = body.Replace("[nombreTecnico]", nombreTecnico.ToString());
    				body = body.Replace("[msj]", msj.ToString());
    				body = body.Replace("[YearActual]", DateTime.Now.Year.ToString());
    				body = body.Replace("[URLCompras]", URLCompras.ToString());
    			}
    			if (estado == 2)
    			{
    				receiverEmail = new MailAddress(emailSolicitante, "Receiver");
    				sub = "El tecnico en camino -ticket # " + id;
    				body = "Hola: " + nombreSolicitante + " el tenico a confirmado la atencion de su ticket.";
    				body = body + "<br><hr>El tecnico " + nombreTecnico + " del area de " + msj + " esta en camino para atender su ticket, asegurece de estar en lugar que indico para su atencion<br><hr>El numero de su ticket es " + id + "<br><hr>Saludos...";
    			}
    			if (estado == 3)
    			{
    				receiverEmail = new MailAddress(emailSolicitante, "Receiver");
    				sub = "Confirmacion de Atencion -ticket # " + id;
    				body = "Hola: " + nombreSolicitante + " el tenico a confirmado que su ticket ya fue atendido.";
    				body = body + "<br><hr>El tecnico " + nombreTecnico + " a confirmado la atencion de su ticket con numero de referencia:" + id + "<br><br><hr>Detalles de atencion de tickets por parte del tecnico:<br> - Procedimiento realizado: <br>" + msj + "<br><br><hr>Un agente de call center se estara comunicando con usted para confirmar la atencion a su ticket. <br><hr>Saludos...";
    			}
    			SmtpClient smtp = GetSmtpClient();
    			using (MailMessage mess = new MailMessage(GetSenderMail(), receiverEmail)
    			{
    				Subject = sub,
    				Body = body,
    				IsBodyHtml = true
    			})
    			{
    				smtp.Send(mess);
    				mess.Dispose();
    			}
    			smtp.Dispose();
    		}
    		catch (Exception)
    		{
    		}
    	}

    	private MailAddress GetSenderMail()
    	{
    		return new MailAddress("notificaciones@asamblea.gob.pa", "Sistema de compras - Asamblea Nacional de Panamá");
    	}

    	private SmtpClient GetSmtpClient()
    	{
    		return new SmtpClient
    		{
    			Host = "correo.asamblea.gob.pa",
    			Port = 587,
    			EnableSsl = false,
    			DeliveryMethod = SmtpDeliveryMethod.Network,
    			UseDefaultCredentials = false,
    			Credentials = new NetworkCredential(GetSenderMail().Address, "Panama2021")
    		};
    	}
    }

}

