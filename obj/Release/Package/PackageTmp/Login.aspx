<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="InternalLims.Login" %>

<!DOCTYPE html>

<html lang="en">
<head>
    <meta charset="utf-8">
    <title>Novogenomics -&amp;Admin </title>
    <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no">
    <meta content="Novogenomics Admin" name="description">
    <meta content="NovoAdmin" name="author">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">

    <!-- App favicon -->
    <link rel="shortcut icon" href="assets/images/mainlogo.png">
    <!-- App css -->
    <link href="assets/css/bootstrap.min.css" rel="stylesheet" type="text/css">
    <link href="assets/css/icons.min.css" rel="stylesheet" type="text/css">
    <link href="assets/css/app.min.css" rel="stylesheet" />
</head>

<body class="account-body accountbg">
    <!-- Recover-pw page -->
    <div class="container">
        <div class="row vh-100 d-flex justify-content-center">
            <div class="col-12 align-self-center">
                <div class="row">
                    <div class="col-lg-5 mx-auto">
                        <div class="card">
                            <div class="card-body p-0 auth-header-box">
                                <div class="text-center p-3">
                                    <a href="index.html" class="logo logo-admin">
                                        <img src="assets/images/mainlogo_white.png" height="80" alt="logo" class="auth-logo">
                                    </a>
                                    <h4 class="mt-3 mb-1 fw-semibold text-white font-18">Novogenomics Admin</h4>
                                </div>
                            </div>
                            <div class="card-body">
                                <form class="form-horizontal auth-form" action="index.html">
                                    <div class="form-group mb-0 row">
                                        <div class="col-12 mt-2">
                                           <a href="Default.aspx"><button class="btn btn-primary w-100 waves-effect waves-light" type="button" style="background-color: #2A2C5A; font-size: 24px">Office 365 <i class="fas fa-sign-in-alt ms-1"></i></button></a> 
                                        </div>
                                        <!--end col-->
                                    </div>
                                    <!--end form-group-->
                                </form>
                                <!--end form-->
                            </div>
                            <div class="card-body bg-light-alt text-center">
                                <span class="text-muted d-none d-sm-inline-block">Novogenomics ©
                                    <script>
                                        document.write(new Date().getFullYear())
                                    </script>
                                    </span>
                            </div>
                        </div>
                        <!--end card-->
                    </div>
                    <!--end col-->
                </div>
                <!--end row-->
            </div>
            <!--end col-->
        </div>
        <!--end row-->
    </div>
</body>
</html>