<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ErrPage.aspx.cs" Inherits="InternalLims.ErrPage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <meta http-equiv="X-UA-Compatible" content="ie=edge">
    <title>Novogenomics. KSA</title>
    <link href="assets/css/app.css" rel="stylesheet" />
    <link href="assets/css/bootstrap.css" rel="stylesheet" />
    <link rel="shortcut icon" href="assets/images/mainlogo.png" />
</head>
<body class="account-body accountbg">
    <div class="container">
        <div class="row vh-100 d-flex justify-content-center">
            <div class="col-12 align-self-center">
                <div class="row">
                    <div class="col-lg-5 mx-auto">
                        <div class="card">
                            <div class="card-body p-0 auth-header-box">
                                <div class="text-center p-3">
                                    <a href="index.html" class="logo logo-admin">
                                        <img src="assets/images/mainlogo_white.png" height="75" alt="logo" class="auth-logo">
                                    </a>
                                </div>
                            </div>
                            <div class="card-body">
                                <div class="ex-page-content text-center">
                                    <img src="assets/images/error.svg" alt="0" class="" height="170">
                                    <h3 class="mt-5">Oops! Sorry, Some error has occurred.</h3>
                                    <h5 class="font-16 text-muted">We are working to fix it soon.</h5>
                                </div>
                                <a class="btn btn-primary w-100 waves-effect waves-light" href="Login">Back to Login <i class="fas fa-redo ms-1"></i></a>
                            </div>
                            <div class="card-body bg-light-alt text-center">
                                <span class="text-muted d-none d-sm-inline-block">Novogenomics ©
                                    <script>
                                        document.write(new Date().getFullYear())
                                    </script>
                                </span>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</body>
</html>
