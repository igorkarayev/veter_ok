<!DOCTYPE html>
<%@ Import Namespace="Delivery.BLL.Helpers" %>    <html>
        <head>
            <meta http-equiv="content-type" content="text/html; charset=UTF8" />

            <title>Вы используете устаревший браузер.</title>
            
            <link rel="icon" type="image/png" href="<%=ResolveClientUrl("~/Styles/Images/favicon.png") %>" />
            <link rel="stylesheet" type="text/css" href="/css/al/common.css?473" />
            <link href='http://fonts.googleapis.com/css?family=Open+Sans:400,400italic,700&subset=cyrillic,cyrillic-ext,latin' rel='stylesheet'>

            <style>
            html, body {
              width: 100%;
              height: 100%;
              background: #F7F7F7;
              padding: 0px;
              margin: 0px;
              font-family: "Open Sans",sans-serif;
              font-size: 87.5%;
              background-image: url("Styles/Images/logo.png");
            }
            #bad_browser {
                position: absolute;
              left: 50%;
              top: 40%;
              text-align: center;
                -ms-opacity: 0.9;
                opacity: 0.9;
              width: 530px;
              margin: -200px 0px 0px -250px;
              background: #FFF;
              line-height: 180%;
              border-bottom: 1px solid #E4E4E4;
              -webkit-box-shadow: 0 0 3px rgba(0, 0, 0, 0.15);
              -moz-box-shadow: 0 0 3px rgba(0, 0, 0, 0.15);
                -ms-box-shadow: 0 0 3px rgba(0, 0, 0, 0.15);
                box-shadow: 0 0 3px rgba(0, 0, 0, 0.15);
                -ms-border-radius: 15px;
                border-radius: 15px;
            }

            #content {
              padding: 20px;
              font-size: 1.19em;
            }
            #head {
              height: 150px;
              background:  url(Styles/Images/logo2.png) no-repeat 70px 50%;
            }
            #content div {
              margin: 10px 0 15px 0;
            }
            #content #browsers {
              width: 480px;
              height: 136px;
              margin: 15px auto 0px;
            }
            #browsers a {
              float: left;
              width: 120px;
              height: 20px;
              padding: 106px 0px 13px 0;
              -webkit-border-radius: 4px;
              -khtml-border-radius: 4px;
              -moz-border-radius: 4px;
              border-radius: 4px;
            }
            #browsers a:hover {
              text-decoration: none;
              background-color: #edf1f5!important;
            }
           

             a {
                color: #2b587a;
                cursor: pointer;
                text-decoration: none;
            }

             hr.styleHR {
                border: 0;
                height: 1px;
                background-image: -webkit-linear-gradient(left, rgba(0,0,0,0), rgba(0,0,0,0.75), rgba(0,0,0,0));
                background-image:    -moz-linear-gradient(left, rgba(0,0,0,0), rgba(0,0,0,0.75), rgba(0,0,0,0));
                background-image:     -ms-linear-gradient(left, rgba(0,0,0,0), rgba(0,0,0,0.75), rgba(0,0,0,0));
                background-image:      -o-linear-gradient(left, rgba(0,0,0,0), rgba(0,0,0,0.75), rgba(0,0,0,0));
            }
            </style>
            <!--[if lte IE 8]>
            <style>
            #bad_browser {
              border: none;
            }
            #wrap {
              border: solid #C3C3C3;
              border-width: 0px 1px 1px;
            }
            #content {
              border: solid #D9E0E7;
              border-width: 0px 1px 1px;
            }
            </style>
            <![endif]-->
        </head>

        <body class="">

        <div id="bad_browser">
          <a href='<%= string.Format("http://{0}",BackendHelper.TagToValue("current_admin_app_address")) %>'><div id="head" class="head"></div></a>
            <hr class="styleHR"/>
          <div id="wrap"><div id="content">
            Для работы с сайтом необходима поддержка <b>Javascript</b> и <b>Cookies</b>.
            <div>
              Чтобы использовать все возможности сайта, загрузите и установите один из этих браузеров:
              <div id="browsers" style="width: 360px;"><a href="http://www.mozilla-europe.org/" target="_blank" style="background: url(<%=ResolveClientUrl("~/Images/firefox.png")%>) no-repeat 50% 17px;">Firefox</a><a href="http://www.opera.com/" target="_blank" style="background: url(<%= ResolveClientUrl("~/Images/opera.png") %>) no-repeat 50% 15px;">Opera</a><a href="http://www.google.com/chrome/" target="_blank" style="background: url(<%=ResolveClientUrl("~/Images/chrome.png")%>) no-repeat 50% 17px;">Chrome</a></div>
            </div>
              Или включите поддержку <b>Javascript</b> и <b>Cookies</b> в вашем браузере.
          </div></div>
        </div>

        </body>
    </html>


