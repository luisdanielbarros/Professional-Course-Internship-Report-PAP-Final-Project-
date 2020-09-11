<%@ Page Title="" Language="C#" MasterPageFile="~/Menu.master" AutoEventWireup="true" CodeFile="Lojas.aspx.cs" Inherits="Lojas" MaintainScrollPositionOnPostBack="true"%>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <title>Lojas</title>
    <!--Custom CSS-->      
    <link href="Assets/Lojas.css" rel="stylesheet" />
    <%--Select2--%>
    <link href="Assets/select2-4.0.3/dist/css/select2.min.css" rel="stylesheet" />
    <style>
       #map {
        height: 600px;
        width: 100%;
       }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <br /><br /><br />
    <div class="container">
        <div class="row">
            <div class="col-md-12">
                <div class="login-panel panel panel-primary" runat="server">
                    <div class="panel-heading">
                        <h4 class="panel-title">Lojas perto de si</h4>
                    </div>
                    <div class="panel-body">
                        <div class="form-group">
                            <asp:DropDownList class="form-control" ID="DropDownListLojas" runat="server">
                                <asp:ListItem>Amadora</asp:ListItem>
                                <asp:ListItem>Queluz</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <br />
                     <div id="map"></div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
    <%--Select2--%>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/select2/4.0.3/js/select2.min.js"></script>
    <script type="text/javascript">
        $("#<%=DropDownListLojas.ClientID%>").select2();

    </script>
        <script>
      function initMap() {
          var uluru = { lat: 38.7585, lng: -9.235478 };
        var map = new google.maps.Map(document.getElementById('map'), {
          zoom: 18,
          center: uluru
        });
        //var image = 'https://developers.google.com/maps/documentation/javascript/examples/full/images/beachflag.png';
        var marker = new google.maps.Marker({
          position: uluru,
          map: map,
          title: "Supermercado BomValor",
          animation: google.maps.Animation.DROP
          //icon: image
        });
        map.addListener('center_changed', function () {
            // 5 seconds after the center of the map has changed, pan back to the
            // marker.
            window.setTimeout(function () {
                map.panTo(marker.getPosition());
            }, 5000);
        });
        marker.addListener('click', toggleBounce);
        marker.setMap(map);
        function toggleBounce() {
            if (marker.getAnimation() !== null) {
                marker.setAnimation(null);
            } else {
                marker.setAnimation(google.maps.Animation.BOUNCE);
            }
        }
        $("#<%=DropDownListLojas.ClientID%>").change(function () {
            if ($("#<%=DropDownListLojas.ClientID%> :selected").val() == "Amadora")
            {
                marker.setPosition(new google.maps.LatLng(38.7585, -9.235478));
                map.panTo(new google.maps.LatLng(38.7585, -9.235478));
            }
            else if ($("#<%=DropDownListLojas.ClientID%> :selected").val() == "Queluz")
            {
                marker.setPosition(new google.maps.LatLng(38.75857, -9.26461));
                map.panTo(new google.maps.LatLng(38.75857, -9.26461));
            }
        });
      }
    </script>
    <script async defer
    src="https://maps.googleapis.com/maps/api/js?key=AIzaSyArdaC_A5xr4o8UoQZ57vD0VTjsN4notT4&callback=initMap">
    </script>
</asp:Content>

