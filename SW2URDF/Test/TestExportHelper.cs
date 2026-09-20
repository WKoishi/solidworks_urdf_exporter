using SolidWorks.Interop.sldworks;
using SolidWorks.Interop.swconst;
using SW2URDF.URDF;
using SW2URDF.URDFExport;
using System.Collections.Generic;
using Xunit;

namespace SW2URDF.Test
{
    [Collection("Requires SW Test Collection")]
    public class TestExportHelper : SW2URDFTest
    {
        public TestExportHelper(SWTestFixture fixture) : base(fixture)
        {
        }

        [Theory]
        [InlineData("3_DOF_ARM", 4, MeshExportFormat.STL)]
        [InlineData("4_WHEELER", 5, MeshExportFormat.STL)]
        [InlineData("ORIGINAL_3_DOF_ARM", 4, MeshExportFormat.STL)]
        [InlineData("3_DOF_ARM", 4, MeshExportFormat.THREEDXML)]
        [InlineData("4_WHEELER", 5, MeshExportFormat.THREEDXML)]
        [InlineData("ORIGINAL_3_DOF_ARM", 4, MeshExportFormat.THREEDXML)]
        public void TestExportRobot(string modelName, int expNumLinks, MeshExportFormat meshExportFormat)
        {
            ModelDoc2 doc = OpenSWDocument(modelName);
            ExportHelper helper = new ExportHelper(SwApp);
            helper.SetComputeInertial(true);
            helper.SetComputeJointKinematics(true);
            helper.SetComputeJointLimits(true);
            helper.SetComputeVisualCollision(true);
            LinkNode baseNode = ConfigurationSerialization.LoadBaseNodeFromModel(doc, out bool error);
            Assert.False(error);
            helper.CreateRobotFromTreeView(baseNode);
            helper.ExportRobot(true, meshExportFormat);
            Assert.NotNull(helper.URDFRobot);
            Assert.Equal(expNumLinks, CommonSwOperations.GetCount(helper.URDFRobot.BaseLink));
            Assert.True(SwApp.CloseAllDocuments(true));
        }

        [Theory]
        [InlineData("3_DOF_ARM", 4)]
        [InlineData("4_WHEELER", 5)]
        [InlineData("ORIGINAL_3_DOF_ARM", 4)]
        public void TestExportRobotNoSTL(string modelName, int expNumLinks)
        {
            ModelDoc2 doc = OpenSWDocument(modelName);
            ExportHelper helper = new ExportHelper(SwApp);
            helper.SetComputeInertial(true);
            helper.SetComputeJointKinematics(true);
            helper.SetComputeJointLimits(true);
            helper.SetComputeVisualCollision(true);
            LinkNode baseNode = ConfigurationSerialization.LoadBaseNodeFromModel(doc, out bool error);
            Assert.False(error);
            helper.CreateRobotFromTreeView(baseNode);
            helper.ExportRobot(false);
            Assert.NotNull(helper.URDFRobot);
            Assert.Equal(expNumLinks, CommonSwOperations.GetCount(helper.URDFRobot.BaseLink));
            Assert.True(SwApp.CloseAllDocuments(true));
        }

        [Theory]
        [InlineData("3_DOF_ARM", 4)]
        [InlineData("4_WHEELER", 5)]
        [InlineData("ORIGINAL_3_DOF_ARM", 4)]
        public void TestExportRobotSkipInertial(string modelName, int expNumLinks)
        {
            ModelDoc2 doc = OpenSWDocument(modelName);
            ExportHelper helper = new ExportHelper(SwApp);
            helper.SetComputeInertial(false);
            helper.SetComputeJointKinematics(true);
            helper.SetComputeJointLimits(true);
            helper.SetComputeVisualCollision(true);
            LinkNode baseNode = ConfigurationSerialization.LoadBaseNodeFromModel(doc, out bool error);
            Assert.False(error);
            helper.CreateRobotFromTreeView(baseNode);
            helper.ExportRobot(true);
            Assert.NotNull(helper.URDFRobot);
            Assert.Equal(expNumLinks, CommonSwOperations.GetCount(helper.URDFRobot.BaseLink));
            Assert.True(SwApp.CloseAllDocuments(true));
        }

        [Theory]
        [InlineData("3_DOF_ARM", 4)]
        [InlineData("4_WHEELER", 5)]
        [InlineData("ORIGINAL_3_DOF_ARM", 4)]
        public void TestExportRobotSkipVisual(string modelName, int expNumLinks)
        {
            ModelDoc2 doc = OpenSWDocument(modelName);
            ExportHelper helper = new ExportHelper(SwApp);
            helper.SetComputeInertial(true);
            helper.SetComputeJointKinematics(true);
            helper.SetComputeJointLimits(true);
            helper.SetComputeVisualCollision(false);
            LinkNode baseNode = ConfigurationSerialization.LoadBaseNodeFromModel(doc, out bool error);
            Assert.False(error);
            helper.CreateRobotFromTreeView(baseNode);
            helper.ExportRobot(true);
            Assert.NotNull(helper.URDFRobot);
            Assert.Equal(expNumLinks, CommonSwOperations.GetCount(helper.URDFRobot.BaseLink));
            Assert.True(SwApp.CloseAllDocuments(true));
        }

        [Theory]
        [InlineData("3_DOF_ARM", 4)]
        [InlineData("4_WHEELER", 5)]
        [InlineData("ORIGINAL_3_DOF_ARM", 4)]
        public void TestExportRobotSkipKinematics(string modelName, int expNumLinks)
        {
            ModelDoc2 doc = OpenSWDocument(modelName);
            ExportHelper helper = new ExportHelper(SwApp);
            helper.SetComputeInertial(true);
            helper.SetComputeJointKinematics(false);
            helper.SetComputeJointLimits(true);
            helper.SetComputeVisualCollision(true);
            LinkNode baseNode = ConfigurationSerialization.LoadBaseNodeFromModel(doc, out bool error);
            Assert.False(error);
            helper.CreateRobotFromTreeView(baseNode);
            helper.ExportRobot(true);
            Assert.NotNull(helper.URDFRobot);
            Assert.Equal(expNumLinks, CommonSwOperations.GetCount(helper.URDFRobot.BaseLink));
            Assert.True(SwApp.CloseAllDocuments(true));
        }

        [Theory]
        [InlineData("3_DOF_ARM", 4)]
        [InlineData("4_WHEELER", 5)]
        [InlineData("ORIGINAL_3_DOF_ARM", 4)]
        public void TestExportRobotSkipLimits(string modelName, int expNumLinks)
        {
            ModelDoc2 doc = OpenSWDocument(modelName);
            ExportHelper helper = new ExportHelper(SwApp);
            helper.SetComputeInertial(true);
            helper.SetComputeJointKinematics(true);
            helper.SetComputeJointLimits(false);
            helper.SetComputeVisualCollision(true);
            LinkNode baseNode = ConfigurationSerialization.LoadBaseNodeFromModel(doc, out bool error);
            Assert.False(error);
            helper.CreateRobotFromTreeView(baseNode);
            helper.ExportRobot(true);
            Assert.NotNull(helper.URDFRobot);
            Assert.Equal(expNumLinks, CommonSwOperations.GetCount(helper.URDFRobot.BaseLink));
            Assert.True(SwApp.CloseAllDocuments(true));
        }

        [Theory]
        [InlineData("3_DOF_ARM", 3)]
        [InlineData("4_WHEELER", 4)]
        [InlineData("ORIGINAL_3_DOF_ARM", 3)]
        public void TestGetJointNames(string modelName, int expNumJoints)
        {
            ModelDoc2 doc = OpenSWDocument(modelName);
            ExportHelper helper = new ExportHelper(SwApp);
            LinkNode baseNode = ConfigurationSerialization.LoadBaseNodeFromModel(doc, out bool error);
            Assert.False(error);
            helper.CreateRobotFromTreeView(baseNode);
            helper.ExportRobot(true);
            List<string> jointNames = helper.GetJointNames();
            Assert.NotNull(jointNames);
            Assert.Equal(jointNames.Count, expNumJoints);
            Assert.True(SwApp.CloseAllDocuments(true));
        }

        /*
         * TODO(SIMINT-164) Part document tests not working (OpenSWPartDocument)
        [Theory]
        [InlineData("TOY_BLOCK")]
        public void TestExportLink(string modelName)
        {
            ModelDoc2 doc = OpenSWPartDocument(modelName);
            ExportHelper helper = new ExportHelper(SwApp);
            helper.ExportLink(true);
            Assert.True(true, "Part export failed");
            Assert.True(SwApp.CloseAllDocuments(true));
        }

        [Theory]
        [InlineData("TOY_BLOCK")]
        public void TestCreateRobotFromActiveModel(string modelName)
        {
            ModelDoc2 doc = OpenSWPartDocument(modelName);
            ExportHelper helper = new ExportHelper(SwApp);
            helper.CreateRobotFromActiveModel();
            Assert.NotNull(helper.URDFRobot);
            Assert.True(SwApp.CloseAllDocuments(true));
        }
        */

        [Theory]
        [InlineData("3_DOF_ARM")]
        public void TestCreateRobotFromTreeView(string modelName)
        {
            ModelDoc2 doc = OpenSWDocument(modelName);
            ExportHelper helper = new ExportHelper(SwApp);
            LinkNode baseNode = ConfigurationSerialization.LoadBaseNodeFromModel(doc, out bool error);
            Assert.False(error);

            helper.CreateRobotFromTreeView(baseNode);
            Assert.NotNull(helper.URDFRobot);
            Assert.True(SwApp.CloseAllDocuments(true));
        }

        [Theory]
        [InlineData("3_DOF_ARM", new double[] { 0, 0, 1 }, "global_origin", new double[] { 0, 0, 1 })]
        public void TestLocalizeAxis(string modelName, double[] axis, string coordSys, double[] expected)
        {
            OpenSWDocument(modelName);
            ExportHelper helper = new ExportHelper(SwApp);
            Assert.Equal(expected, helper.LocalizeAxis(axis, coordSys));
            Assert.True(SwApp.CloseAllDocuments(true));
        }

        [Theory]
        [InlineData("3_DOF_ARM", new string[] {
            "Origin_global",
            "Origin_prox_joint",
            "Origin_dist_joint",
            "Origin_effector_joint" })]
        public void TestGetRefCoordinateSystems(string modelName, string[] expected)
        {
            OpenSWDocument(modelName);
            ExportHelper helper = new ExportHelper(SwApp);
            Assert.Equal(new List<string>(expected), helper.GetRefCoordinateSystems());
            Assert.True(SwApp.CloseAllDocuments(true));
        }

        [Theory]
        [InlineData("3_DOF_ARM", new string[] {
            "Axis_prox_joint",
            "Axis_dist_joint",
            "Axis_effector_joint" })]
        public void TestGetRefAxes(string modelName, string[] expected)
        {
            OpenSWDocument(modelName);
            ExportHelper helper = new ExportHelper(SwApp);
            Assert.Equal(new List<string>(expected), helper.GetRefAxes());
            Assert.True(SwApp.CloseAllDocuments(true));
        }

        /// <summary>
        /// A link's exported mass and moment of inertia must match what SolidWorks itself reports
        /// for that link's components, which is what a user compares the export against.
        /// Computing these from the components' bodies instead silently drops mass properties the
        /// user has overridden, and misplaces bodies that belong to a subassembly.
        ///
        /// Both quantities asserted here are independent of the coordinate system: mass trivially
        /// so, and the trace of the inertia tensor about the center of mass because it is
        /// invariant under rotation. That keeps the test from having to resolve the link's
        /// coordinate system, which for a subassembly lives in a subcomponent.
        /// </summary>
        [Theory]
        [InlineData("3_DOF_ARM")]
        [InlineData("4_WHEELER")]
        [InlineData("ORIGINAL_3_DOF_ARM")]
        public void TestInertialMatchesSolidWorks(string modelName)
        {
            ModelDoc2 doc = OpenSWDocument(modelName);
            ExportHelper helper = new ExportHelper(SwApp);
            helper.SetComputeInertial(true);
            helper.SetComputeJointKinematics(true);
            helper.SetComputeJointLimits(true);
            helper.SetComputeVisualCollision(true);
            LinkNode baseNode = ConfigurationSerialization.LoadBaseNodeFromModel(doc, out bool error);
            Assert.False(error);

            // The saved configuration stores components as persistent IDs. The export UI resolves
            // them before building the robot, so do the same here.
            List<string> problemLinks = new List<string>();
            CommonSwOperations.LoadSWComponents(doc, baseNode, problemLinks);
            Assert.Empty(problemLinks);

            Assert.True(helper.CreateRobotFromTreeView(baseNode));

            double totalMass = doc.Extension.CreateMassProperty().Mass;
            int linksChecked = 0;

            foreach (Link link in Flatten(helper.URDFRobot.BaseLink))
            {
                if (link.SWComponents.Count == 0)
                {
                    continue;
                }

                IMassProperty2 expected = (IMassProperty2)doc.Extension.CreateMassProperty2();
                Assert.NotNull(expected);
                expected.UseSystemUnits = true;
                expected.IncludeHiddenBodiesOrComponents = true;
                expected.SelectedItems = link.SWComponents.ToArray();
                Assert.True(expected.Recalculate());

                Assert.Equal(expected.Mass, link.Inertial.Mass.Value, 9);

                double[] expectedMoment = (double[])expected.GetMomentOfInertia(
                    (int)swMomentsOfInertiaReferenceFrame_e.swMomentsOfInertiaReferenceFrame_CenterOfMass);
                double expectedTrace = expectedMoment[0] + expectedMoment[4] + expectedMoment[8];
                double actualTrace = link.Inertial.Inertia.Ixx +
                    link.Inertial.Inertia.Iyy + link.Inertial.Inertia.Izz;
                Assert.Equal(expectedTrace, actualTrace, 12);

                Assert.True(link.Inertial.Mass.Value > 0.0, "link " + link.Name + " has no mass");

                // An ignored or empty selection degrades into the whole document's properties.
                // No single link of these robots is the entire assembly.
                Assert.True(link.Inertial.Mass.Value < totalMass,
                    "link " + link.Name + " has the mass of the whole assembly");

                linksChecked++;
            }

            Assert.True(linksChecked > 0, "no links were checked for " + modelName);
            Assert.True(SwApp.CloseAllDocuments(true));
        }

        private static IEnumerable<Link> Flatten(Link link)
        {
            yield return link;
            foreach (Link child in link.Children)
            {
                foreach (Link descendant in Flatten(child))
                {
                    yield return descendant;
                }
            }
        }
    }
}