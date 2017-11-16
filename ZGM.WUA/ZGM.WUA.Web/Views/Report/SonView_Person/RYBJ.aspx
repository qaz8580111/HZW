<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RYBJ.aspx.cs" Inherits="ZGM.WUA.Web.Views.Report.SonView_Person.RYBJ" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
<link href="../../../Styles/report.css" rel="stylesheet" />
</head>
<body>
   <div class="top_RYBJ">
       <div class="title_RYBJ"> 
           报警类型统计
       </div>
       <div class="tabDiv_RYBJ" style=" text-align:center;">
           <div class="tab tab_left_selected">日</div>
            <div class="tab ">月</div>
            <div class="tab ">年</div>
       </div>
   </div>
    <div class="content_1" id="pie_chartDiv">

    </div>
      <div class="content_2" id="bar_chartDiv">

    </div>
    
</body>
</html>
<script src="../../../Scripts/base/jquery-1.8.2.min.js"></script>
<script src="../../../Scripts/Chart/echarts.min.js"></script>
<script src="../../../Scripts/Report/SonView_Person/RYBJ.js"></script>
