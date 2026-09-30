<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MyJsonTest.aspx.cs" Inherits="SendBulkJsonObj.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style>
        .container {
            border: 1px solid rgb(73, 72, 72);
            border-radius: 10px;
            margin: auto;
            padding: 10px;
            text-align: center;
        }

        button {
            border-radius: 5px;
            padding: 10px;
            color: #fff;
            background-color: #167deb;
            border-color: #0062cc;
            font-weight: bolder;
            cursor: pointer;
        }

            button:hover {
                text-decoration: none;
                background-color: #0069d9;
                border-color: #0062cc;
            }
    </style>
    <!-- jQuery Ajax CDN -->
    <script src="https://cdnjs.cloudflare.com/ajax/libs/jquery/3.6.0/jquery.min.js">
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <h2 style="color: green">MyApp</h2>
            <div class="container">
                <b>Pass multiple JSON objects</b>
                <br />
                <br />
                <!-- Button to multiple JSON objects -->
                <button type="button" id="btn">
                    Click on me!
                </button>
                <div style="height: 10px"></div>
                <div id="resultID"></div>
            </div>
        </div>
    </form>
    <script>

        $(document).ready(() => {

            // Adding 'click' event listener to button
            $("#btn").click(() => {

                // Two JSON objects are passed to server	
                let obj1 = 
                       {
        "godown_id": "23370030312",
        "godown_name": "Shri Kapura Wh Alonia 96",
        "depot_id": "2337003",
        "password": "d18b40d204f55b6f92f44c6e847a7a9df34c4ed503cf6085cb006b3422038743"
    };
                

                // jQuery Ajax Post Request using $.ajax()
                
                    $.ajax({
                        url: 'https://scm.mp.gov.in/getgodowndetails/GodownService/GodownApp/newgodown',
                        type: 'POST',
                        // passing JSON objects as comma(,) separated values
                        data: {
                            obj1

                        },
                        success: (response) => {
                            //response	
                            $("#resultID").show("Data saved successfully..!");
                        }
                    })
                
            });
        });

    </script>
</body>
</html>
