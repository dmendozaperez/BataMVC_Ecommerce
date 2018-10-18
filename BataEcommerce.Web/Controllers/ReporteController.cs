using BataEcommerceWebReport.Helpers;
using Microsoft.Reporting.WebForms;
using System;
using System.Web.Mvc;
using BataEcommerce.Util;
using System.Web.UI.WebControls;
using BataEcommerce.Web.Filters;
using System.Collections.Generic;
using BataEcommerce.BL.Components.Prestashop;
using BataEcommerce.Web.Models;
using BataEcommerce.BE.Prestashop;
using System.Linq;

namespace BataEcommerce.Web.Controllers
{
    [FilterSecurity]
    public class ReporteController : Controller
    {
        public string reportServerUrl = "http://" + Configuration.GetParameter("BataRSHost") + ':' + Configuration.GetParameter("BataRSHostPort") + "/" + Configuration.GetParameter("BataRSServer") + "/";
        public ReportCredentials reportCredential = new ReportCredentials(Configuration.GetParameter("BataRSUser"), Configuration.GetParameter("BataRSPass"), Configuration.GetParameter("BataRSHost"));
        public string reportFolder = Configuration.GetParameter("BataRSFolder");

      

       
        public ActionResult ReporteVentasView()
        {
            ViewBag.Title = "Reporte de Pedidos Ecommerce";
            ViewBag.SubTitle = "Lista los Pedidos Importados de Tienda Virtual.";
            return View();
        }
        public ActionResult ReporteVentas(string pFecDes, string pFecHas, string pEst, string pCli)
        {
            // ** Procesar Parametros
            string Param_FechaDesde = (pFecDes == "") ? "" : pFecDes;
            string Param_FechaHasta = (pFecHas == "") ? "" : pFecHas;
            string Param_Estado = (pEst == "") ? "" : pEst;
            string Param_Cliente = (pCli == "") ? "" : pCli;

            List<ReportParameter> paramList = new List<ReportParameter>();
            paramList.Add(new ReportParameter("Param_FechaDesde", Param_FechaDesde, false));
            paramList.Add(new ReportParameter("Param_FechaHasta", Param_FechaHasta, false));
            paramList.Add(new ReportParameter("Param_Estado", Param_Estado, false));
            paramList.Add(new ReportParameter("Param_Cliente", Param_Cliente, false));

            // ** Procesar Reporte en Servidor
            ReportViewer reportViewer = new ReportViewer();
            reportViewer.ProcessingMode = ProcessingMode.Remote;
            reportViewer.SizeToReportContent = true;
            reportViewer.ZoomMode = ZoomMode.FullPage;
            reportViewer.Width = Unit.Percentage(100);
            reportViewer.Height = Unit.Percentage(100);
            reportViewer.AsyncRendering = true;

            reportViewer.ServerReport.ReportServerCredentials = reportCredential;
            reportViewer.ServerReport.ReportServerUrl = new Uri(reportServerUrl);
            reportViewer.ServerReport.ReportPath = "/" + reportFolder + "/RptPedidoEcommerce";
            reportViewer.ShowParameterPrompts = false;
            reportViewer.ServerReport.SetParameters(paramList);
            reportViewer.ServerReport.Refresh();
            ViewBag.ReportViewer = reportViewer;
            ViewBag.Title = "Reporte de Pedidos Ecommerce";
            return View();
        }
        public ActionResult ReporteStocksView()
        {
            ViewBag.Title = "Reporte de Stocks de Productos";
            ViewBag.SubTitle = "Consulta el Stock Actual de Tienda Virtual.";
            return View();
        }
        public ActionResult ReporteStocks(string pProcod, string pPronom, string pCateg, string pTalla, string pSigno, string pStock)
        {
            // ** Procesar Parametros
            string Param_Reference = (pProcod == "") ? "" : pProcod;
            string Param_ProductName = (pPronom == "") ? "" : pPronom;
            string Param_CategoryName = (pCateg == "") ? "" : pCateg;
            string Param_Talla = (pTalla == "") ? "" : pTalla;
            string Param_Signo = (pSigno == "") ? "" : pSigno;
            string Param_Stock = (pStock == "") ? "-1" : pStock;

            List<ReportParameter> paramList = new List<ReportParameter>();
            paramList.Add(new ReportParameter("Param_Reference", Param_Reference, false));
            paramList.Add(new ReportParameter("Param_ProductName", Param_ProductName, false));
            paramList.Add(new ReportParameter("Param_CategoryName", Param_CategoryName, false));
            paramList.Add(new ReportParameter("Param_Talla", Param_Talla, false));
            paramList.Add(new ReportParameter("Param_Signo", Param_Signo, false));
            paramList.Add(new ReportParameter("Param_Stock", Param_Stock, false));


            // ** Procesar Reporte en Servidor
            ReportViewer reportViewer = new ReportViewer();
            reportViewer.ProcessingMode = ProcessingMode.Remote;
            reportViewer.SizeToReportContent = true;
            reportViewer.ZoomMode = ZoomMode.FullPage;
            reportViewer.Width = Unit.Percentage(100);
            reportViewer.Height = Unit.Percentage(100);
            reportViewer.AsyncRendering = true;

            reportViewer.ServerReport.ReportServerCredentials = reportCredential;
            reportViewer.ServerReport.ReportServerUrl = new Uri(reportServerUrl);
            reportViewer.ServerReport.ReportPath = "/" + reportFolder + "/RptStockProducto";
            reportViewer.ShowParameterPrompts = false;
            reportViewer.ServerReport.SetParameters(paramList);
            reportViewer.ServerReport.Refresh();
            ViewBag.ReportViewer = reportViewer;
            ViewBag.Title = "Reporte de Stocks de Productos";
            return View();
        }
        public ActionResult ReportePedidoView()
        {
            ViewBag.Title = "Reporte de Pedidos Caídos";
            ViewBag.SubTitle = "Lista de Pedidos Caídos.";
            return View();
        }
        public ActionResult ReportePedido(string pFecDes, string pFecHas, string pEst, string pCli)
        {
            // ** Procesar Parametros
            string Param_FechaDesde = (pFecDes == "") ? "1900-01-01" : pFecDes;
            string Param_FechaHasta = (pFecHas == "") ? "1900-01-01" : pFecHas;
            string Param_Estado = (pEst == "") ? "-1" : pEst;
            string Param_Cliente = (pCli == "") ? "" : pCli;

            List<ReportParameter> paramList = new List<ReportParameter>();
            paramList.Add(new ReportParameter("Param_FechaDesde", Param_FechaDesde, false));
            paramList.Add(new ReportParameter("Param_FechaHasta", Param_FechaHasta, false));
            paramList.Add(new ReportParameter("Param_Estado", Param_Estado, false));
            paramList.Add(new ReportParameter("Param_Cliente", Param_Cliente, false));

            // ** Procesar Reporte en Servidor
            ReportViewer reportViewer = new ReportViewer();
            reportViewer.ProcessingMode = ProcessingMode.Remote;
            reportViewer.SizeToReportContent = true;
            reportViewer.ZoomMode = ZoomMode.FullPage;
            reportViewer.Width = Unit.Percentage(100);
            reportViewer.Height = Unit.Percentage(100);
            reportViewer.AsyncRendering = true;

            reportViewer.ServerReport.ReportServerCredentials = reportCredential;
            reportViewer.ServerReport.ReportServerUrl = new Uri(reportServerUrl);
            reportViewer.ServerReport.ReportPath = "/" + reportFolder + "/RptPedidosCaidos";
            reportViewer.ShowParameterPrompts = false;
            reportViewer.ServerReport.SetParameters(paramList);
            reportViewer.ServerReport.Refresh();
            ViewBag.ReportViewer = reportViewer;
            ViewBag.Title = "Reporte de Pedidos Caídos";
            return View();
        }
        public ActionResult ReporteRankingView()
        {
            ViewBag.Title = "Reporte de Ranking de Productos";
            ViewBag.SubTitle = "Lista los Pedidos en Orden de Mayor Venta de Tienda Virtual.";
            return View();
        }
        public ActionResult ReporteRanking(string pProcod, string pPronom, string pCateg, string pTalla, string pSigno, string pVenta)
        {
            // ** Procesar Parametros
            string Param_Reference = (pProcod == "") ? "" : pProcod;
            string Param_ProductName = (pPronom == "") ? "" : pPronom;
            string Param_CategoryName = (pCateg == "") ? "" : pCateg;
            string Param_Talla = (pTalla == "") ? "" : pTalla;
            string Param_Signo = (pSigno == "") ? "" : pSigno;
            string Param_Stock = (pVenta == "") ? "-1" : pVenta;

            List<ReportParameter> paramList = new List<ReportParameter>();
            paramList.Add(new ReportParameter("Param_Reference", Param_Reference, false));
            paramList.Add(new ReportParameter("Param_ProductName", Param_ProductName, false));
            paramList.Add(new ReportParameter("Param_CategoryName", Param_CategoryName, false));
            paramList.Add(new ReportParameter("Param_Talla", Param_Talla, false));
            paramList.Add(new ReportParameter("Param_Signo", Param_Signo, false));
            paramList.Add(new ReportParameter("Param_Stock", Param_Stock, false));

            // ** Procesar Reporte en Servidor
            ReportViewer reportViewer = new ReportViewer();
            reportViewer.ProcessingMode = ProcessingMode.Remote;
            reportViewer.SizeToReportContent = true;
            reportViewer.ZoomMode = ZoomMode.FullPage;
            reportViewer.Width = Unit.Percentage(100);
            reportViewer.Height = Unit.Percentage(100);
            reportViewer.AsyncRendering = true;

            reportViewer.ServerReport.ReportServerCredentials = reportCredential;
            reportViewer.ServerReport.ReportServerUrl = new Uri(reportServerUrl);
            reportViewer.ServerReport.ReportPath = "/" + reportFolder + "/RptRankingProducto";
            reportViewer.ShowParameterPrompts = false;
            reportViewer.ServerReport.SetParameters(paramList);
            reportViewer.ServerReport.Refresh();
            ViewBag.ReportViewer = reportViewer;
            ViewBag.Title = "Reporte de Ranking de Productos";
            return View();
        }


        public ActionResult VentasResumidoView()
        {
            ViewBag.Title = "Reporte de Ventas Resumido Ecommerce";
            ViewBag.SubTitle = "Lista las Ventas Realizadas en el Año comparadas con el Anterior.";
            return View();
        }
        public ActionResult VentasResumido(string pAno)
        {
            // ** Procesar Parametros
            string ANIOACT = (pAno == "") ? "" : pAno;

            List<ReportParameter> paramList = new List<ReportParameter>();
            paramList.Add(new ReportParameter("ANIOACT", ANIOACT, false));

            // ** Procesar Reporte en Servidor
            ReportViewer reportViewer = new ReportViewer();
            reportViewer.ProcessingMode = ProcessingMode.Remote;
            reportViewer.SizeToReportContent = true;
            reportViewer.ZoomMode = ZoomMode.FullPage;
            reportViewer.Width = Unit.Percentage(100);
            reportViewer.Height = Unit.Percentage(100);
            reportViewer.AsyncRendering = true;

            reportViewer.ServerReport.ReportServerCredentials = reportCredential;
            reportViewer.ServerReport.ReportServerUrl = new Uri(reportServerUrl);
            reportViewer.ServerReport.ReportPath = "/" + reportFolder + "/RptVtaResumido";
            reportViewer.ShowParameterPrompts = false;
            reportViewer.ServerReport.SetParameters(paramList);
            reportViewer.ServerReport.Refresh();
            ViewBag.ReportViewer = reportViewer;
            ViewBag.Title = "Reporte de Ventas Resumido Ecommerce";
            return View();
        }
    }
}