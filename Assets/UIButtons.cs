using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIButtons : MonoBehaviour
{
    public GameObject createAccountMenu;
    public GameObject logIntoAccountMenu;
    public GameObject forgotAccountMenu;
    public GameObject recoverAccountMenu;
    public GameObject changePasswordMenu;

    public void CreateButton()
    {
        SetAllInactive();
        createAccountMenu.SetActive(true);
    }

    public void LoginButton()
    {
        SetAllInactive();
        logIntoAccountMenu.SetActive(true);
    }

    public void ForgotButton()
    {
        SetAllInactive();
        forgotAccountMenu.SetActive(true);
    }

    public void BackLoginButton()
    {
        SetAllInactive();
        createAccountMenu.SetActive(true);
    }

    public void BackForgotButton()
    {
        SetAllInactive();
        logIntoAccountMenu.SetActive(true);
    }

    public void RequestCodeButton()
    {
        SetAllInactive();
        recoverAccountMenu.SetActive(true);
    }

    public void SubmitCodeButton()
    {
        SetAllInactive();
        changePasswordMenu.SetActive(true);
    }

    public void SetAllInactive()
    {
        createAccountMenu.SetActive(false);
        logIntoAccountMenu.SetActive(false);
        forgotAccountMenu.SetActive(false);
        recoverAccountMenu.SetActive(false);
        changePasswordMenu.SetActive(false);
    }
}
