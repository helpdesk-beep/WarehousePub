<%@ Page Language="C#" AutoEventWireup="true" CodeFile="~/Default6.aspx.cs" Inherits="Default6" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Generate Bills</title>
    <script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/jspdf/2.5.1/jspdf.umd.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/html2canvas/1.4.1/html2canvas.min.js"></script>
    <style>
        .EU_DataTable {
            width: 100%;
            border-collapse: collapse;
            margin: 15px 0;
        }

            .EU_DataTable th, .EU_DataTable td {
                border: 1px solid black;
                padding: 5px;
                text-align: center;
                font-size: 11px;
            }

        .header-section {
            border-bottom: 3px solid black;
            text-align: center;
            padding-bottom: 10px;
            margin-bottom: 10px;
        }

        #loader {
            display: none;
            position: fixed;
            top: 50%;
            left: 50%;
            transform: translate(-50%, -50%);
            z-index: 1000;
            background: rgba(0,0,0,0.7);
            color: white;
            padding: 20px;
            border-radius: 10px;
        }

        .bill-page {
            width: 800px;
            padding: 30px;
            margin: 20px auto;
            background: #fff;
            border: 1px solid #ccc;
            font-family: Cambria;
            /* Optimization: helps browser rendering engine */
            contain: paint;
            overflow: hidden;
        }
    </style>
</head>
<body>
    <div id="loader" style="display: none; position: fixed; top: 50%; left: 50%; transform: translate(-50%, -50%); z-index: 1000; background: rgba(0,0,0,0.85); color: white; padding: 30px; border-radius: 12px; text-align: center;">
        <div class="spinner" style="margin-bottom: 10px;"></div>
        <div id="progressText" style="font-weight: bold; font-size: 16px;">Processing Bills...</div>
    </div>
    <div id="loaderOverlay" style="display: none; position: fixed; top: 50%; left: 50%; transform: translate(-50%, -50%); z-index: 1000; background: rgba(0,0,0,0.7); color: white; padding: 20px; border-radius: 10px;">
        Processing Bills... Please wait.
    </div>

    <div style="text-align: center; padding: 20px;">
        <button type="button" id="btnGenerate" style="display: none; background-color: #007bff; color: white; padding: 8px 18px; border: none; border-radius: 6px; cursor: pointer;">
            Download All Bills (PDF)
   
        </button>
    </div>

    <div id="billsContainer"></div>


    <script type="text/javascript">
            $(document).ready(function () {
                // 
                loadAllBills();
            });

            async function loadAllBills() {
                // 
                $("#loaderOverlay").show();

                $.ajax({
                    type: "POST",
                    url: "Default6.aspx/GetAllBillsData",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (res) {
                        if (res.d && res.d.length > 0) {
                            // 
                            renderBills(res.d);

                            // ✅ DATA IS READY: Show the PDF button now 
                            $("#btnGenerate").fadeIn();
                        } else {
                            alert("No bills found.");
                        }
                        // 
                        $("#loaderOverlay").hide();
                    },
                    error: function (err) {
                        console.error("Error fetching data", err);
                        $("#loaderOverlay").hide();
                    }
                });
            }

            function renderBills(bills) {
                // Corrected to match your HTML div ID [cite: 23, 62]
                const container = $("#billsContainer");
                container.empty();

                // - Keep your existing bill format exactly as it is
                bills.forEach(bill => {
                    let billHtml = `
        <div class="bill-page" style="padding:40px; background:white; margin-bottom:20px; font-family:Cambria; border:1px solid #ccc;">
            <div style="text-align:center; border-bottom:3px solid black;">
                <strong>M.P. Warehousing & Logistics Corporation - Bhopal<br/>STORAGE BILL</strong><br/>
                <span>GST: 23AADCM7742B3ZS | PAN: AADCM7742B</span><br/>
                Region: ${bill.Header.Regionnm} | District: ${bill.Header.District_Name} | Branch: ${bill.Header.DepotName}
            </div>

            <table border="1" style="width:100%; border-collapse:collapse; margin-top:10px;">
                <tr><td colspan="3">Warehouse: <b>${bill.Header.Godown_Name} (${bill.Header.Godown_Id})</b></td></tr>
                <tr>
                    <td style="text-align:left;">Month: ${bill.Header.Bill_Month}<br/>Bill No: ${bill.Header.Bill_Number}</td>
                    <td style="text-align:left;">Depositor: NAFED-BHOPAL<br/>Commodity: ${bill.Header.Commodity}</td>
                    <td style="text-align:left;">Rate: Rs. ${bill.Header.Rate} PER BAG</td>
                </tr>
            </table>

            <table class="EU_DataTable" border="1" style="width:100%; border-collapse:collapse; margin-top:15px; text-align:center;">
                <thead style="background:#f2f2f2;">
                    <tr><th>Commodity</th><th>Month</th><th>Period</th><th>Opening</th><th>In</th><th>Out</th><th>Closing</th><th>Total</th></tr>
                </thead>
                <tbody>
                    ${bill.Grid.map(row => `
                        <tr>
                            <td>${row.Commodity}</td><td>${row.Bill_Month}</td><td>${row.Dates_Period}</td>
                            <td>${row.Opening}</td><td>${row.In}</td><td>${row.Out}</td>
                            <td>${row.Closing}</td><td>${row.Total}</td>
                        </tr>
                    `).join('')}
                </tbody>
            </table>

            <div style="display:flex; justify-content:space-between; margin-top:20px;">
                ${bill.DSC.map(d => `
                    <div style="text-align:right; font-size:9pt; border:1px solid #ddd; padding:5px;">
                        <img src="../images/dsc1.png" width="60" /><br/>
                        <b>DSC (${d.Type == 'B' ? 'BM' : 'RM'})</b><br/>
                        ${d.Name}<br/>IP: ${d.IP}<br/>Date: ${d.Date}
                    </div>
                `).join('')}
            </div>
            <div style="text-align:center; color:red; font-size:8pt; border-top:1px solid black; margin-top:10px;">
                *This bill is digitally signed, no stamp required.
            </div>
        </div>`;
                    container.append(billHtml);
                });

                const images = container.find('img');
                let loadedCount = 0;

                if (images.length === 0) {
                    window.print();
                } else {
                    images.on('load error', function () {
                        loadedCount++;
                        if (loadedCount === images.length) {
                            // 2. Trigger print once all DSC images are ready
                            window.print();
                        }
                    });
                }
            }

            // PDF Generation Trigger
            //$("#btnGenerate").click(async function () {
            //    const { jsPDF } = window.jspdf;
            //    const doc = new jsPDF("p", "pt", "a4");
            //    const pages = document.querySelectorAll(".bill-page");

            //    $("#loaderOverlay").show();
            //    //
            //    for (let i = 0; i < pages.length; i++) {
            //        //
            //        const canvas = await html2canvas(pages[i], { scale: 2 });
            //        const imgData = canvas.toDataURL("image/jpeg", 0.8);
            //        const pageWidth = doc.internal.pageSize.getWidth();
            //        const imgHeight = (canvas.height * (pageWidth - 40)) / canvas.width;

            //        //
            //        if (i > 0) doc.addPage();
            //        doc.addImage(imgData, 'JPEG', 20, 20, pageWidth - 40, imgHeight);
            //    }
            //    doc.save("Nafed_Bills_ClientSide.pdf");
            //    $("#loaderOverlay").hide();
            //});
        $("#btnGenerate").click(async function () {
            const { jsPDF } = window.jspdf;
            // Standard A4 settings for faster calculation
            const doc = new jsPDF("p", "pt", "a4");
            const pages = document.querySelectorAll(".bill-page");
            const totalPages = pages.length;

            if (totalPages === 0) return;

            // Show loader and disable button
            $("#loader").show();
            $(this).attr("disabled", true).css("opacity", "0.6");

            for (let i = 0; i < totalPages; i++) {
                // Update Progress Counter
                $("#progressText").text(`Generating Page ${i + 1} of ${totalPages}...`);

                // OPTIMIZATION: Scale 1.5 is significantly faster than 2.0
                const canvas = await html2canvas(pages[i], {
                    scale: 1.5,
                    useCORS: true,
                    logging: false,
                    imageTimeout: 0
                });

                // OPTIMIZATION: JPEG 0.7 quality provides high speed and small file size
                const imgData = canvas.toDataURL("image/jpeg", 0.7);
                const pageWidth = doc.internal.pageSize.getWidth();
                const margin = 20;
                const width = pageWidth - (margin * 2);
                const height = (canvas.height * width) / canvas.width;

                if (i > 0) doc.addPage(); // [cite: 75]

                // Use 'FAST' compression for embedding images
                doc.addImage(imgData, 'JPEG', margin, margin, width, height, undefined, 'FAST');

                // MEMORY CLEANUP: Clear canvas memory immediately
                canvas.width = 0;
                canvas.height = 0;
            }

            // Save and Reset UI
            doc.save("Nafed_Bills_Optimized.pdf");
            $("#loader").hide();
            $("#btnGenerate").attr("disabled", false).css("opacity", "1");
            $("#progressText").text("Processing Bills...");
        });
    </script>
</body>
</html>
