<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Report.aspx.cs" Inherits="ZGM.WUA.Web.Views.Report.Report" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>报表中心</title>
    <link href="../../Styles/report.css" rel="stylesheet" />
</head>
<body>
    <div id="contentDiv">
        <iframe id="ifmContent" src="" style="height: 100%; width: 100%; border: 0px"></iframe>
    </div>
    <div id="bottomDiv">
       <ul class="slide-nav">     
            <%--  <li class="selected">1</li>    
              <li class="">2</li>    
              <li class="">3</li>       
              <li class="">4</li>       
              <li class="">5</li> 
              <li class="">6</li>  
              <li class="">7</li>   
              <li class="">8</li> 
              <li class="">9</li> --%>
       </ul>
    </div>
</body>
<script src="../../Scripts/base/jquery-1.8.2.min.js"></script>
<script src="../../Scripts/Report/report.js"></script>
</html>
