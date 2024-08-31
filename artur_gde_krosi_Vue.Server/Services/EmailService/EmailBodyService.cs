using System;
using System.Web;

namespace artur_gde_krosi_Vue.Server.Services.EmailService
{
    public class EmailBodyService
    {
        public string EmailBodyRestEmail(string username, string email, string token, string domain)
        {
            string encodedEmail = HttpUtility.UrlEncode(email);
            string encodedToken = HttpUtility.UrlEncode(token);

            return @$"<!DOCTYPE html
  PUBLIC ""-//W3C//DTD XHTML 1.0 Transitional//EN"" ""http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd"">
<html xmlns=""http://www.w3.org/1999/xhtml"" xmlns:v=""urn:schemas-microsoft-com:vml""
  xmlns:o=""urn:schemas-microsoft-com:office:office"" lang=""ru"">

<head>
  <meta http-equiv=""Content-Type"" content=""text/html; charset=utf-8"" />
  <meta http-equiv=""X-UA-Compatible"" content=""IE=edge"" />
  <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"" />
  <meta name=""color-scheme"" content=""light dark"" />
  <meta name=""supported-color-schemes"" content=""light dark"" />
  <script src=""https://kit.fontawesome.com/0d90cb17ec.js"" crossorigin=""anonymous""></script>
  <title>Артур, где мои кроссовки? - Подтверждение E-mail</title>
  <style type=""text/css"">
    @import url('https://fonts.googleapis.com/css2?family=Poppins:wght@400;500;600&display=swap');
    @import url('https://fonts.googleapis.com/css2?family=Inter:wght@100..900&display=swap');
    @import url('https://fonts.googleapis.com/css2?family=Oswald:wght@200..700&display=swap');

    table {{
      border-spacing: 0;
      mso-cellspacing: 0;
      mso-padding-alt: 0;
    }}

    td {{
      padding: 0;
    }}

    #outlook a {{
      padding: 0;
    }}

    a {{
      text-decoration: none;
      color: #e8fbfa;
      font-size: 16px;
    }}

    @media screen and (max-width: 599.98px) {{}}

    @media screen and (max-width: 399.98px) {{
      .mobile-padding {{
        padding-right: 10px !important;
        padding-left: 10px !important;
      }}

      .mobile-col-padding {{
        padding-right: 0 !important;
        padding-left: 0 !important;
      }}

      .two-columns .column {{
        width: 100% !important;
        max-width: 100% !important;
      }}

      .two-columns .column img {{
        width: 100% !important;
        max-width: 100% !important;
      }}

      .three-columns .column {{
        width: 100% !important;
        max-width: 100% !important;
      }}

      .three-columns .column img {{
        width: 100% !important;
        max-width: 100% !important;
      }}
    }}

    /* Custom Dark Mode Colors */
    :root {{
      color-scheme: light dark;
      supported-color-schemes: light dark;
    }}

    @media (prefers-color-scheme: dark) {{

      table,
      td {{
        color: #06080B !important;
      }}

      h1,
      h2,
      h3,
      p {{
        color: #06080B !important;
      }}
    }}

    .bordered-button-default {{
      font-size: 18px;
      padding: 10px 20px;
      background-color: #fafdfe;
      border: 1px solid #D2A805;
      color: #D2A805;
      transition: 0.2s;
    }}

    .bordered-button-default:hover {{
      cursor: pointer;
      background-color: #fff3a1;
    }}
  </style>
</head>

<body style=""Margin:0; padding:0; min-width:100%; background-color:#dde0e1;"">
    <center style=""width: 100%; table-layout:fixed; background-color: #dde0e1; padding-top: 40px; padding-bottom: 40px;"">
        <div style=""max-width: 600px; background-color: #fafdfe; box-shadow: 0 0 10px rgba(0, 0, 0, .2);"">

          <div style=""font-size: 0px;color: #fafdfe;line-height: 1px; mso-line-height-rule:exactly; display: none; max-width: 0px; max-height: 0px; opacity: 0; overflow: hidden;  mso-hide:all;"">
            Подтверждение E-mail адреса для доступа к Вашему аккаунту.
          </div>
          
          <table align=""center"" border=""0"" cellspacing=""0"" cellpadding=""0"" role=""presentation"" style=""color:#1C1E23; font-family: 'Poppins',sans-serif, Arial, Helvetica; background-color: #fafdfe; Margin:0; padding:0; width: 100%; max-width: 600px;"">
            <tr>
              <td style=""background-color: #1C1E23; color: #fafdfe; padding: 5px 10px;"">
              </td>
            </tr>
            <tr>
              <th style=""background-color: #FDDD00; padding: 10px 20px;"" align=""center"">
                <img src=""https://bucetimg.storage.yandexcloud.net/Logo_1.png"" alt=""Logo"" style=""width: 300px;"">
              </th>
            </tr>
            <tr>
              <td>
                <table align=""center"" border=""0"" cellspacing=""0"" cellpadding=""0"" role=""presentation"" style=""color:#1C1E23; font-family: 'Poppins',sans-serif, Arial, Helvetica; background-color: #fafdfe; Margin:0; padding:25px; width: 100%; max-width: 600px;"">
                  <tr>
                    <td style=""padding: 10px 20px; padding-top: 20px;"">Уважаемый(ая) " + username + @$",</td>
                  </tr>
                  <tr>
                    <td style=""padding: 10px 20px;"">
                      Добро пожаловать в наш интернет-магазин! 
                      Мы рады видеть вас среди наших клиентов.</td>
                  </tr>
                  <tr>
                    <td style=""padding: 10px 20px; padding-bottom: 0;"">
                      Для завершения процесса регистрации вашего аккаунта, 
                      пожалуйста, подтвердите ваш E-mail адрес, 
                      нажав на кнопку ниже:</td>
                  </tr>
                  <tr>
                    <td align=""center"" style=""padding: 50px 0; border-bottom: 1px solid #D2A805;"">
                      <a href="""+domain+"/#/regEmailConfirmation/" + encodedEmail + "/"+ encodedToken + @$""" target=""_blank""><button class=""bordered-button-default"">Подтвердить E-mail</button></a>
                    </td>
                  </tr>
                </table>
              </td>
            </tr>
            <tr>
              <td>
                <table align=""center"" border=""0"" cellspacing=""0"" cellpadding=""0"" role=""presentation"" style=""color:#1C1E23; font-family: 'Poppins',sans-serif, Arial, Helvetica; background-color: #fafdfe; Margin:0; padding: 25px; padding-top: 0; width: 100%; max-width: 600px;"">
                  <tr>
                    <td style=""padding: 0 20px;"">
                      Подтвержденный E-mail адрес даёт Вам возможность входить в свой аккаунт, 
                      редактировать данные в Вашем личном кабинете, 
                      а также позволяет предотвратить другие неудобства:
                    </td>
                  </tr>
                  <tr>
                    <td>
                      <ul style=""font-size: 15px; padding: 0 30px; padding-left: 45px;"">
                        <li style=""padding: 10px 0;"">Подтвержденный E-mail адрес помогает защитить ваш аккаунт от несанкционированного доступа.</li>
                        <li style=""padding: 10px 0;"">Мы сможем связываться с Вами посредством E-mail для отправки уведомлений о заказах, акциях и информации о товарах.</li>
                        <li style=""padding: 10px 0;"">В случае утери пароля или других проблем с доступом к аккаунту, подтвержденный E-mail адрес позволит Вам восстановить доступ.</li>
                      </ul>
                    </td>
                  </tr>
                </table>
              </td>
            </tr>
            <tr>
              <td>
                <table align=""center"" border=""0"" cellspacing=""0"" cellpadding=""0"" role=""presentation"" style=""color:#fafdfe; font-family: 'Poppins',sans-serif, Arial, Helvetica; background-color: #1C1E23; Margin:0; padding: 15px 20px; width: 100%; max-width: 600px;"">
                  <tr>
                    
                  </tr>
                </table>
              </td>
            </tr>
          </table>
        </div>
    </center>    
</body>
</html>";
        }

        public string EmailBodyPasswordReset(string username, string email, string token, string domain)
        {
            string encodedEmail = HttpUtility.UrlEncode(email);
            string encodedToken = HttpUtility.UrlEncode(token);

            return @$"<!DOCTYPE html
  PUBLIC ""-//W3C//DTD XHTML 1.0 Transitional//EN"" ""http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd"">
<html xmlns=""http://www.w3.org/1999/xhtml"" xmlns:v=""urn:schemas-microsoft-com:vml""
  xmlns:o=""urn:schemas-microsoft-com:office:office"" lang=""ru"">

<head>
  <meta http-equiv=""Content-Type"" content=""text/html; charset=utf-8"" />
  <meta http-equiv=""X-UA-Compatible"" content=""IE=edge"" />
  <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"" />
  <meta name=""color-scheme"" content=""light dark"" />
  <meta name=""supported-color-schemes"" content=""light dark"" />
  <script src=""https://kit.fontawesome.com/0d90cb17ec.js"" crossorigin=""anonymous""></script>
  <title>Артур, где мои кроссовки? - Изменение пароля</title>
  <style type=""text/css"">
    @import url('https://fonts.googleapis.com/css2?family=Poppins:wght@400;500;600&display=swap');
    @import url('https://fonts.googleapis.com/css2?family=Inter:wght@100..900&display=swap');
    @import url('https://fonts.googleapis.com/css2?family=Oswald:wght@200..700&display=swap');

    table {{
      border-spacing: 0;
      mso-cellspacing: 0;
      mso-padding-alt: 0;
    }}

    td {{
      padding: 0;
    }}

    #outlook a {{
      padding: 0;
    }}

    a {{
      text-decoration: none;
      color: #e8fbfa;
      font-size: 16px;
    }}

    @media screen and (max-width: 599.98px) {{}}

    @media screen and (max-width: 399.98px) {{
      .mobile-padding {{
        padding-right: 10px !important;
        padding-left: 10px !important;
      }}

      .mobile-col-padding {{
        padding-right: 0 !important;
        padding-left: 0 !important;
      }}

      .two-columns .column {{
        width: 100% !important;
        max-width: 100% !important;
      }}

      .two-columns .column img {{
        width: 100% !important;
        max-width: 100% !important;
      }}

      .three-columns .column {{
        width: 100% !important;
        max-width: 100% !important;
      }}

      .three-columns .column img {{
        width: 100% !important;
        max-width: 100% !important;
      }}
    }}

    /* Custom Dark Mode Colors */
    :root {{
      color-scheme: light dark;
      supported-color-schemes: light dark;
    }}

    @media (prefers-color-scheme: dark) {{

      table,
      td {{
        color: #06080B !important;
      }}

      h1,
      h2,
      h3,
      p {{
        color: #06080B !important;
      }}
    }}

    .bordered-button-default {{
      font-size: 18px;
      padding: 10px 20px;
      background-color: #fafdfe;
      border: 1px solid #D2A805;
      color: #D2A805;
      transition: 0.2s;
    }}

    .bordered-button-default:hover {{
      cursor: pointer;
      background-color: #fff3a1;
    }}
  </style>
</head>

<body style=""Margin:0; padding:0; min-width:100%; background-color:#dde0e1;"">
    <center style=""width: 100%; table-layout:fixed; background-color: #dde0e1; padding-top: 40px; padding-bottom: 40px;"">
        <div style=""max-width: 600px; background-color: #fafdfe; box-shadow: 0 0 10px rgba(0, 0, 0, .2);"">

          <div style=""font-size: 0px;color: #fafdfe;line-height: 1px; mso-line-height-rule:exactly; display: none; max-width: 0px; max-height: 0px; opacity: 0; overflow: hidden;  mso-hide:all;"">
            Изменение пароля от Вашего аккаунта.
          </div>
          
          <table align=""center"" border=""0"" cellspacing=""0"" cellpadding=""0"" role=""presentation"" style=""color:#1C1E23; font-family: 'Poppins',sans-serif, Arial, Helvetica; background-color: #fafdfe; Margin:0; padding:0; width: 100%; max-width: 600px;"">
            <tr>
              <td style=""background-color: #1C1E23; color: #fafdfe; padding: 5px 10px;"">
              </td>
            </tr>
            <tr>
              <th style=""background-color: #FDDD00; padding: 10px 20px;"" align=""center"">
                <img src=""https://bucetimg.storage.yandexcloud.net/Logo_1.png"" alt=""Logo"" style=""width: 300px;"">
              </th>
            </tr>
            <tr>
              <td>
                <table align=""center"" border=""0"" cellspacing=""0"" cellpadding=""0"" role=""presentation"" style=""color:#1C1E23; font-family: 'Poppins',sans-serif, Arial, Helvetica; background-color: #fafdfe; Margin:0; padding:25px; width: 100%; max-width: 600px;"">
                  <tr>
                    <td style=""padding: 10px 20px; padding-top: 20px;"">Уважаемый(ая) " + username + $@",</td>
                  </tr>
                  <tr>
                    <td style=""padding: 10px 20px;"">
                      Мы получили запрос на смену пароля для вашего аккаунта.</td>
                  </tr>
                  <tr>
                    <td style=""padding: 10px 20px; padding-bottom: 0;"">
                      Для изменения  пароля от вашего аккаунта, 
                      пожалуйста, нажмите на кнопку ниже:</td>
                  </tr>
                  <tr>
                    <td align=""center"" style=""padding: 30px 0; border-bottom: 1px solid #D2A805;"">
                      <a href="""+domain+"/#/changePasswordConfirmation/" + encodedEmail + "/"+ encodedToken + $@"""><button class=""bordered-button-default"">Изменить пароль</button></a>
                    </td>
                  </tr>
                </table>
              </td>
            </tr>
            <tr>
              <td>
                <table align=""center"" border=""0"" cellspacing=""0"" cellpadding=""0"" role=""presentation"" style=""color:#1C1E23; font-family: 'Poppins',sans-serif, Arial, Helvetica; background-color: #fafdfe; Margin:0; padding: 25px; padding-top: 0; width: 100%; max-width: 600px;"">
                  <tr>
                    <td style=""padding: 0 20px; text-align: center;"">
                      Если вы не инициировали изменение пароля или обнаружили какие-либо подозрительные действия в своем аккаунте, пожалуйста, немедленно свяжитесь с нами.
                    </td>
                  </tr>
                </table>
              </td>
            </tr>
            <tr>
              <td>
                <table align=""center"" border=""0"" cellspacing=""0"" cellpadding=""0"" role=""presentation"" style=""color:#fafdfe; font-family: 'Poppins',sans-serif, Arial, Helvetica; background-color: #1C1E23; Margin:0; padding: 15px 20px; width: 100%; max-width: 600px;"">
                  <tr>
                    
                  </tr>
                </table>
              </td>
            </tr>
          </table>
        </div>
    </center>    
</body>
</html>";
        }
    }
}
