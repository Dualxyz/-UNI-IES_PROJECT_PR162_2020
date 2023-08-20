﻿using System;
using System.Collections.Generic;
using CIM.Model;
using FTN.Common;
using FTN.ESI.SIMES.CIM.CIMAdapter.Manager;

namespace FTN.ESI.SIMES.CIM.CIMAdapter.Importer
{
	/// <summary>
	/// PowerTransformerImporter
	/// </summary>
	public class PowerTransformerImporter
	{
		/// <summary> Singleton </summary>
		private static PowerTransformerImporter ptImporter = null;
		private static object singletoneLock = new object();

		private ConcreteModel concreteModel;
		private Delta delta;
		private ImportHelper importHelper;
		private TransformAndLoadReport report;


		#region Properties
		public static PowerTransformerImporter Instance
		{
			get
			{
				if (ptImporter == null)
				{
					lock (singletoneLock)
					{
						if (ptImporter == null)
						{
							ptImporter = new PowerTransformerImporter();
							ptImporter.Reset();
						}
					}
				}
				return ptImporter;
			}
		}

		public Delta NMSDelta
		{
			get 
			{
				return delta;
			}
		}
		#endregion Properties


		public void Reset()
		{
			concreteModel = null;
			delta = new Delta();
			importHelper = new ImportHelper();
			report = null;
		}

		public TransformAndLoadReport CreateNMSDelta(ConcreteModel cimConcreteModel)
		{
			LogManager.Log("Importing PowerTransformer Elements...", LogLevel.Info);
			report = new TransformAndLoadReport();
			concreteModel = cimConcreteModel;
			delta.ClearDeltaOperations();

			if ((concreteModel != null) && (concreteModel.ModelMap != null))
			{
				try
				{
					// convert into DMS elements
					ConvertModelAndPopulateDelta();
				}
				catch (Exception ex)
				{
					string message = string.Format("{0} - ERROR in data import - {1}", DateTime.Now, ex.Message);
					LogManager.Log(message);
					report.Report.AppendLine(ex.Message);
					report.Success = false;
				}
			}
			LogManager.Log("Importing PowerTransformer Elements - END.", LogLevel.Info);
			return report;
		}

		/// <summary>
		/// Method performs conversion of network elements from CIM based concrete model into DMS model.
		/// </summary>
		private void ConvertModelAndPopulateDelta()
		{
			LogManager.Log("Loading elements and creating delta...", LogLevel.Info);

			//// import all concrete model types (DMSType enum)
			/// [Q] Pogledaj kako se implementiraju Konkretne klase			
	

          
            ImportSeriesCompensator();
            ImportACLineSegment();
            ImportDCLineSegment();
            ImportConnectivityNodeContainer();
            ImportConnectivityNode();
            ImportTerminal();




            LogManager.Log("Loading elements and creating delta completed.", LogLevel.Info);
		}

        #region Import
        #region TERMINAL
        private void ImportTerminal()
		{
			SortedDictionary<string, object> cimTerminals = concreteModel.GetAllObjectsOfType("FTN.Terminal");
			if (cimTerminals != null)
			{
				foreach (KeyValuePair<string, object> cimTerminalPair in cimTerminals)
				{
					FTN.Terminal cimTerminal = cimTerminalPair.Value as FTN.Terminal;

					ResourceDescription rd = CreateTerminalResourceDescription(cimTerminal);
					if (rd != null)
					{
						delta.AddDeltaOperation(DeltaOpType.Insert, rd, true);
						report.Report.Append("Terminal ID = ").Append(cimTerminal.ID).Append(" SUCCESSFULLY converted to GID = ").AppendLine(rd.Id.ToString());
					}
					else
					{
						report.Report.Append("Terminal ID = ").Append(cimTerminal.ID).AppendLine(" FAILED to be converted");
					}
				}
				report.Report.AppendLine();
			}
		}

		private ResourceDescription CreateTerminalResourceDescription(FTN.Terminal cimTerminal)
		{
			ResourceDescription rd = null;
			if (cimTerminal != null)
			{
				long gid = ModelCodeHelper.CreateGlobalId(0, (short)DMSType.TERMINAL, importHelper.CheckOutIndexForDMSType(DMSType.TERMINAL));
				rd = new ResourceDescription(gid);
				importHelper.DefineIDMapping(cimTerminal.ID, gid);

				////populate ResourceDescription
				PowerTransformerConverter.PopulateTerminalProperties(cimTerminal, rd, importHelper, report);
			}
			return rd;
		}
        #endregion TERMINAL

        #region CONNECTIVITYNODECONTAINER
        //NEW CLASS ==================================================================================================================
        private void ImportConnectivityNodeContainer()
        {
            SortedDictionary<string, object> cimConnectivityNodeContainers = concreteModel.GetAllObjectsOfType("FTN.ConnectivityNodeContainer");
            if (cimConnectivityNodeContainers != null)
            {
                foreach (KeyValuePair<string, object> cimConnectivityNodeContainerPair in cimConnectivityNodeContainers)
                {
                    FTN.ConnectivityNodeContainer cimConnectivityNodeContainer = cimConnectivityNodeContainerPair.Value as FTN.ConnectivityNodeContainer;

                    ResourceDescription rd = CreateConnectivityNodeContainerResourceDescription(cimConnectivityNodeContainer);
                    if (rd != null)
                    {
                        delta.AddDeltaOperation(DeltaOpType.Insert, rd, true);
                        report.Report.Append("ConnectivityNodeContainer ID = ").Append(cimConnectivityNodeContainer.ID).Append(" SUCCESSFULLY converted to GID = ").AppendLine(rd.Id.ToString());
                    }
                    else
                    {
                        report.Report.Append("ConnectivityNodeContainer ID = ").Append(cimConnectivityNodeContainer.ID).AppendLine(" FAILED to be converted");
                    }
                }
                report.Report.AppendLine();
            }
        }

        private ResourceDescription CreateConnectivityNodeContainerResourceDescription(FTN.ConnectivityNodeContainer cimConnectivityNodeContainer)
        {
            ResourceDescription rd = null;
            if (cimConnectivityNodeContainer != null)
            {
                long gid = ModelCodeHelper.CreateGlobalId(0, (short)DMSType.CONNECTIVITYNODECONTAINER, importHelper.CheckOutIndexForDMSType(DMSType.CONNECTIVITYNODECONTAINER));
                rd = new ResourceDescription(gid);
                importHelper.DefineIDMapping(cimConnectivityNodeContainer.ID, gid);

                ////populate ResourceDescription
                PowerTransformerConverter.PopulateConnectivityNodeContainerProperties(cimConnectivityNodeContainer, rd);
            }
            return rd;
        }
        #endregion

        #region CONNECTIVITYNODE
        private void ImportConnectivityNode()
		{
			SortedDictionary<string, object> cimConnectivityNodes = concreteModel.GetAllObjectsOfType("FTN.ConnectivityNode");
			if (cimConnectivityNodes != null)
			{
				foreach (KeyValuePair<string, object> cimConnectivityNodePair in cimConnectivityNodes)
				{
					FTN.ConnectivityNode cimConnectivityNode = cimConnectivityNodePair.Value as FTN.ConnectivityNode;

					ResourceDescription rd = CreateConnectivityNodeResourceDescription(cimConnectivityNode);
					if (rd != null)
					{
						delta.AddDeltaOperation(DeltaOpType.Insert, rd, true);
						report.Report.Append("ConnectivityNode ID = ").Append(cimConnectivityNode.ID).Append(" SUCCESSFULLY converted to GID = ").AppendLine(rd.Id.ToString());
					}
					else
					{
						report.Report.Append("ConnectivityNode ID = ").Append(cimConnectivityNode.ID).AppendLine(" FAILED to be converted");
					}
				}
				report.Report.AppendLine();
			}
		}

		private ResourceDescription CreateConnectivityNodeResourceDescription(FTN.ConnectivityNode cimConnectivityNode)
		{
			ResourceDescription rd = null;
			if (cimConnectivityNode != null)
			{
				long gid = ModelCodeHelper.CreateGlobalId(0, (short)DMSType.CONNECTIVITYNODE, importHelper.CheckOutIndexForDMSType(DMSType.CONNECTIVITYNODE));
				rd = new ResourceDescription(gid);
				importHelper.DefineIDMapping(cimConnectivityNode.ID, gid);

				////populate ResourceDescription
				PowerTransformerConverter.PopulateConnectivityNodeProperties(cimConnectivityNode, rd, importHelper, report);
			}
			return rd;
		}
        #endregion

        #region SERIESCOMPENSATOR
        //NEW CLASS ==================================================================================================================
        private void ImportSeriesCompensator()
        {
            SortedDictionary<string, object> cimSeriesCompensators = concreteModel.GetAllObjectsOfType("FTN.SeriesCompensator");
            if (cimSeriesCompensators != null)
            {
                foreach (KeyValuePair<string, object> cimSeriesCompensatorPair in cimSeriesCompensators)
                {
                    FTN.SeriesCompensator cimSeriesCompensator = cimSeriesCompensatorPair.Value as FTN.SeriesCompensator;

                    ResourceDescription rd = CreateSeriesCompensatorResourceDescription(cimSeriesCompensator);
                    if (rd != null)
                    {
                        delta.AddDeltaOperation(DeltaOpType.Insert, rd, true);
                        report.Report.Append("SeriesCompensator ID = ").Append(cimSeriesCompensator.ID).Append(" SUCCESSFULLY converted to GID = ").AppendLine(rd.Id.ToString());
                    }
                    else
                    {
                        report.Report.Append("SeriesCompensator ID = ").Append(cimSeriesCompensator.ID).AppendLine(" FAILED to be converted");
                    }
                }
                report.Report.AppendLine();
            }
        }

        private ResourceDescription CreateSeriesCompensatorResourceDescription(FTN.SeriesCompensator cimSeriesCompensator)
        {
            ResourceDescription rd = null;
            if (cimSeriesCompensator != null)
            {
                long gid = ModelCodeHelper.CreateGlobalId(0, (short)DMSType.SERIESCOMPENSATOR, importHelper.CheckOutIndexForDMSType(DMSType.SERIESCOMPENSATOR));
                rd = new ResourceDescription(gid);
                importHelper.DefineIDMapping(cimSeriesCompensator.ID, gid);

                ////populate ResourceDescription
                PowerTransformerConverter.PopulateSeriesCompensatorProperties(cimSeriesCompensator, rd);
            }
            return rd;
        }
        #endregion

        #region ACLINESEGMENT
        //NEW CLASS ==================================================================================================================
        private void ImportACLineSegment()
        {
            SortedDictionary<string, object> cimACLineSegments = concreteModel.GetAllObjectsOfType("FTN.ACLineSegment");
            if (cimACLineSegments != null)
            {
                foreach (KeyValuePair<string, object> cimACLineSegmentPair in cimACLineSegments)
                {
                    FTN.ACLineSegment cimACLineSegment = cimACLineSegmentPair.Value as FTN.ACLineSegment;

                    ResourceDescription rd = CreatecimACLineSegmentResourceDescription(cimACLineSegment);
                    if (rd != null)
                    {
                        delta.AddDeltaOperation(DeltaOpType.Insert, rd, true);
                        report.Report.Append("ACLineSegment ID = ").Append(cimACLineSegment.ID).Append(" SUCCESSFULLY converted to GID = ").AppendLine(rd.Id.ToString());
                    }
                    else
                    {
                        report.Report.Append("ACLineSegment ID = ").Append(cimACLineSegment.ID).AppendLine(" FAILED to be converted");
                    }
                }
                report.Report.AppendLine();
            }
        }

        private ResourceDescription CreatecimACLineSegmentResourceDescription(FTN.ACLineSegment cimACLineSegment)
        {
            ResourceDescription rd = null;
            if (cimACLineSegment != null)
            {
                long gid = ModelCodeHelper.CreateGlobalId(0, (short)DMSType.ACLINESEGMENT, importHelper.CheckOutIndexForDMSType(DMSType.ACLINESEGMENT));
                rd = new ResourceDescription(gid);
                importHelper.DefineIDMapping(cimACLineSegment.ID, gid);

                ////populate ResourceDescription
                PowerTransformerConverter.PopulateACLineSegmentProperties(cimACLineSegment, rd);
            }
            return rd;
        }
        #endregion

        #region DCLINESEGMENT
        //NEW CLASS ==================================================================================================================
        private void ImportDCLineSegment()
        {
            SortedDictionary<string, object> cimDCLineSegments = concreteModel.GetAllObjectsOfType("FTN.DCLineSegment");
            if (cimDCLineSegments != null)
            {
                foreach (KeyValuePair<string, object> cimDCLineSegmentPair in cimDCLineSegments)
                {
                    FTN.DCLineSegment cimDCLineSegment = cimDCLineSegmentPair.Value as FTN.DCLineSegment;

                    ResourceDescription rd = CreatecimDCLineSegmentResourceDescription(cimDCLineSegment);
                    if (rd != null)
                    {
                        delta.AddDeltaOperation(DeltaOpType.Insert, rd, true);
                        report.Report.Append("DCLineSegment ID = ").Append(cimDCLineSegment.ID).Append(" SUCCESSFULLY converted to GID = ").AppendLine(rd.Id.ToString());
                    }
                    else
                    {
                        report.Report.Append("DCLineSegment ID = ").Append(cimDCLineSegment.ID).AppendLine(" FAILED to be converted");
                    }
                }
                report.Report.AppendLine();
            }
        }

        private ResourceDescription CreatecimDCLineSegmentResourceDescription(FTN.DCLineSegment cimDCLineSegment)
        {
            ResourceDescription rd = null;
            if (cimDCLineSegment != null)
            {
                long gid = ModelCodeHelper.CreateGlobalId(0, (short)DMSType.DCLINESEGMENT, importHelper.CheckOutIndexForDMSType(DMSType.DCLINESEGMENT));
                rd = new ResourceDescription(gid);
                importHelper.DefineIDMapping(cimDCLineSegment.ID, gid);

                ////populate ResourceDescription
                PowerTransformerConverter.PopulateDCLineSegmentProperties(cimDCLineSegment, rd);
            }
            return rd;
        }
        #endregion
        #endregion Import
    }
}

