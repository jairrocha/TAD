using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;

namespace TAD_Treinamento_a_distancia.Models
{
    public class Mensagens
    {
        private string statusMsg = "";

        public string EnviaEmail(string EmailDestino, string msg, string assunto)
        {

            string remetenteEmail = "tadtreinamentoadistancia@gmail.com"; //O e-mail do remetente

            MailMessage mail = new MailMessage();

            mail.To.Add(EmailDestino);
            mail.From = new MailAddress(remetenteEmail, "TAD - Treinamento a Distância", System.Text.Encoding.UTF8);
            mail.Subject = assunto;
            mail.SubjectEncoding = System.Text.Encoding.UTF8;
            mail.Body = msg;
            mail.BodyEncoding = System.Text.Encoding.UTF8;
            mail.IsBodyHtml = true;
            mail.Priority = MailPriority.High; //Prioridade do E-Mail

            SmtpClient client = new SmtpClient();  //Adicionando as credenciais do seu e-mail e senha:
            client.Credentials = new System.Net.NetworkCredential(remetenteEmail, "tadtad123");



            client.Port = 587; // Esta porta é a utilizada pelo Gmail para envio
            client.Host = "smtp.gmail.com"; //Definindo o provedor que irá disparar o e-mail
            client.EnableSsl = true; //Gmail trabalha com Server Secured Layer

            try
            {

                client.Send(mail);
                return statusMsg = "Email Enviado";

            }

            catch (SmtpException ex)
            {

                return statusMsg = "Ocorreu um erro no envio";

            }



        }
    }
}