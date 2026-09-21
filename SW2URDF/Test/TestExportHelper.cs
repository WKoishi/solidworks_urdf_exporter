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

        /// <summary>
        /// SetMomentMatrix must copy the products of inertia through unchanged. URDF and the
        /// SolidWorks API agree on the negative convention, so negating them here silently
        /// mirrors every link whose tensor is not diagonal, which no trace- or mass-based
        /// assertion can detect.
        /// </summary>
        [Fact]
        public void TestSetMomentMatrixPreservesProductsOfInertia()
        {
            // Distinct values, mixed signs, so a flipped or transposed term is unambiguous.
            double[] moment = new double[]
            {
                11.0, -12.0, 13.0,
                -12.0, 22.0, -23.0,
                13.0, -23.0, 33.0,
            };

            Inertia inertia = new Inertia();
            inertia.SetMomentMatrix(moment);

            Assert.Equal(11.0, inertia.Ixx);
            Assert.Equal(22.0, inertia.Iyy);
            Assert.Equal(33.0, inertia.Izz);
            Assert.Equal(-12.0, inertia.Ixy);
            Assert.Equal(13.0, inertia.Ixz);
            Assert.Equal(-23.0, inertia.Iyz);

            // GetMoment rebuilds the symmetric matrix that LocalizeLink rotates. A sign change in
            // SetMomentMatrix would not survive that round trip, because negating the products of
            // inertia does not commute with a general rotation.
            Assert.Equal(moment, inertia.GetMoment());
        }

        /// <summary>
        /// The exported products of inertia must match what SolidWorks reports for the link, sign
        /// included. URDF and the SolidWorks API share the negative convention, so these carry
        /// through unchanged; the mass properties dialog's default "positive tensor notation" is a
        /// display setting and not what the API returns.
        ///
        /// This has to compare the products themselves. They are the only part of the tensor a
        /// flipped sign moves appreciably: the trace ignores them outright, and the principal
        /// moments shift only through the Ixy*Ixz*Iyz term of the characteristic polynomial, which
        /// for the near-diagonal links in these models stays under 1e-13 and so drowns in
        /// eigenvalue solver noise.
        ///
        /// The products are not invariant under rotation, so the reference has to be taken in the
        /// link's own coordinate system, resolved through the exporter the same way
        /// ComputeInertialProperties does.
        /// </summary>
        [Theory]
        [InlineData("3_DOF_ARM")]
        [InlineData("ORIGINAL_3_DOF_ARM")]
        public void TestProductsOfInertiaMatchSolidWorks(string modelName)
        {
            ModelDoc2 doc = OpenSWDocument(modelName);
            ExportHelper helper = new ExportHelper(SwApp);
            helper.SetComputeInertial(true);
            helper.SetComputeJointKinematics(true);
            helper.SetComputeJointLimits(true);
            helper.SetComputeVisualCollision(true);
            LinkNode baseNode = ConfigurationSerialization.LoadBaseNodeFromModel(doc, out bool error);
            Assert.False(error);

            List<string> problemLinks = new List<string>();
            CommonSwOperations.LoadSWComponents(doc, baseNode, problemLinks);
            Assert.Empty(problemLinks);

            Assert.True(helper.CreateRobotFromTreeView(baseNode));

            // A flipped sign moves a product of inertia by twice its magnitude. Only terms whose
            // doubled magnitude clears this are evidence either way; the rest are numerical zeros
            // that match under both conventions.
            const double detectable = 1e-12;

            int discriminatingTerms = 0;

            foreach (Link link in Flatten(helper.URDFRobot.BaseLink))
            {
                if (link.SWComponents.Count == 0)
                {
                    continue;
                }

                double[] moment = helper.GetLinkMomentOfInertiaForTest(link);

                Inertia actual = link.Inertial.Inertia;
                Assert.Equal(moment[1], actual.Ixy, 12);
                Assert.Equal(moment[2], actual.Ixz, 12);
                Assert.Equal(moment[5], actual.Iyz, 12);

                foreach (double product in new double[] { moment[1], moment[2], moment[5] })
                {
                    if (2.0 * System.Math.Abs(product) > detectable)
                    {
                        discriminatingTerms++;
                    }
                }
            }

            // Guard against the assertions above passing only because every product of inertia in
            // this model is a numerical zero, which both conventions would satisfy. If this fails,
            // the models no longer pin the convention and one with a genuinely off-diagonal link is
            // needed.
            Assert.True(discriminatingTerms > 0,
                "no product of inertia in " + modelName + " exceeded " + detectable +
                ", so this test cannot tell the two sign conventions apart");

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