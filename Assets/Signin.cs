using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using TMPro;
using UnityEngine.SceneManagement;

using System;
using System.Net;
using System.Net.Mail;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Linq;


public class Signin : MonoBehaviour
{
    public TMP_InputField signupUsernameField;
    public TMP_InputField signupPasswordField;
    public TMP_InputField signupEmailField;
    public TMP_InputField loginUsernameField;
    public TMP_InputField loginPasswordField;
    public TMP_InputField forgotEmailField;
    public TMP_InputField forgotPasswordField;

    public TMP_InputField newPassword;
    public TMP_InputField updateCode;

    public string[] characters = { "a","b","c","d","e","f","g","h","i","j","k","l","m","n","o","p","q","r","s","t","u","v","w","x","y","z","1","2","3","4","5","6","7","8","9","0" };
    public int randomCharacter;
    public string randomCharacters;
    public string randomCode;
    private string user;
    private string password;

    public UnityEngine.UI.Text notification;

    IEnumerator CreateUser(string username, string email, string password)
    {
        if (username != "" && email != "" && password != "")
        {
            string createUserURL = "http://localhost/nsirpg/insertuser.php";
            WWWForm form = new WWWForm();
            form.AddField("username", username);
            form.AddField("email", email);
            form.AddField("password", password);
            UnityWebRequest webRequest = UnityWebRequest.Post(createUserURL, form);

            yield return webRequest.SendWebRequest();
        }
        else
        {
            Debug.Log("BRUH... you aint got no username or email or password");
        }
    }
    public void CreateNewUser()
    {
        StartCoroutine(CreateUser(signupUsernameField.text, signupEmailField.text, signupPasswordField.text));
    }
    IEnumerator Login(string username, string password)
    {
        string createLoginURL = "http://localhost/nsirpg/login.php";
        WWWForm form = new WWWForm();
        form.AddField("username", username);
        form.AddField("password", password);
        UnityWebRequest webRequest = UnityWebRequest.Post(createLoginURL, form);

        yield return webRequest.SendWebRequest();

        Debug.Log(webRequest.downloadHandler.text);

        if (webRequest.downloadHandler.text == "Login Successful")
        {
            SceneManager.LoadScene(1);
        }
    }
    public void NewLogin()
    {
        StartCoroutine(Login(loginUsernameField.text, loginPasswordField.text));
    }

    IEnumerator ForgotUser(TMP_InputField email)
    {
        string forgotURL = "http://localhost/nsirpg/checkEmail.php";
        WWWForm form = new WWWForm();
        form.AddField("email_Post", email.text);
        UnityWebRequest webRequest = UnityWebRequest.Post(forgotURL, form);

        yield return webRequest.SendWebRequest();
        Debug.Log(webRequest.downloadHandler.text);
        if (webRequest.downloadHandler.text == "user not found")
        {
            Debug.Log(webRequest.downloadHandler.text);
        }
        else
        {
            user = webRequest.downloadHandler.text;
            SendEmail(email);
        }
    }
    public void CheckEmail(TMP_InputField email)
    {
        StartCoroutine(ForgotUser(forgotEmailField));
    }

    IEnumerator ForgotPassword(TMP_InputField forgotPasswordField)
    {
        string forgotURL = "http://localhost/nsirpg/UpdatePassword.php";
        WWWForm form = new WWWForm();
        form.AddField("username_Post", user);
        form.AddField("password_Post", forgotPasswordField.text);
        UnityWebRequest webRequest = UnityWebRequest.Post(forgotURL, form);

        yield return webRequest.SendWebRequest();
        Debug.Log(webRequest.downloadHandler.text);
    }
    public void ForgotPassword()
    {
        StartCoroutine(ForgotPassword(forgotPasswordField));
    }

    public void SendEmail(TMP_InputField email)
    {
        RandomCodeGen();
        MailMessage mail = new MailMessage();
        mail.From = new MailAddress("sqlunityclasssydney@gmail.com");
        mail.To.Add(email.text);
        mail.Body = "Hello " + user + "\nReset using this code: " + randomCode;

        SmtpClient smtpServer = new SmtpClient("smtp.gmail.com");
        smtpServer.Port = 25;
        smtpServer.Credentials = new NetworkCredential("sqlunityclasssydney@gmail.com", "sqlpassword") as ICredentialsByHost;
        smtpServer.EnableSsl = true;

        ServicePointManager.ServerCertificateValidationCallback = delegate (object s, X509Certificate cert, X509Chain chain, SslPolicyErrors policyErrors) { return true; };
        smtpServer.Send(mail);
        Debug.Log("Sending mail");
    }

    public void RandomCodeGen()
    {
        randomCode = "";
        for (int c = 0; c < 6; c++)
        {
            randomCharacter = UnityEngine.Random.Range(0, characters.Length);
            randomCharacters = characters[randomCharacter];

            randomCode += randomCharacters;
        }
        Debug.Log(randomCode);
    }
}
