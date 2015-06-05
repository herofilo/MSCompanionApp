using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using jamoram62.tools.MSCompanion;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MSCompanion_UnitTests
{
    [TestClass]
    public class AssetManifest_UnitTest
    {

        private string TestManifestFilename = "manifest_test.xml";

        [TestMethod]
        public void AMUT_001()
        {
            _Initialize(MethodInfo.GetCurrentMethod().Name);

            Trace.WriteLine("AssetManifest: Crea una instancia de un fichero ");

            AssetManifest assetManifest;

            assetManifest = LoadManifest(AssetManifestFileText0);

            assetManifest = LoadManifest(AssetManifestFileText1);

        }


        private AssetManifest LoadManifest(string pText)
        {
            if (File.Exists(TestManifestFilename))
                File.Delete(TestManifestFilename);
            File.WriteAllText(TestManifestFilename, pText.Replace('\'', '"'));

            return AssetManifest.Load(TestManifestFilename);
        }



        // -------------------------------------------------------------------------------------------------------------

        private void _Initialize(string pMethodName)
        {
            Trace.WriteLine(String.Format("TESTMETHOD: {0}", pMethodName));


        }


        // ------------------------------------------------------------------------------------------------------

        private const string AssetManifestFileText0 =
            @"<AssetManifest>
  <propModels>
    <Prop>
      <tags/>
      <templates>
        <Entry>
          <name>Data/Props/Stall/1AD Market Stall.template</name>
        </Entry>
      </templates>
      <parts>
        <ModelPart>
          <slot>0</slot>
          <name>Data/Props/Stall/1AD Market Stall.part</name>
        </ModelPart>
      </parts>
      <skeleton>Stall.CSF</skeleton>
      <meshes>
        <string>Stall.CMF</string>
      </meshes>
      <animations/>
      <name>Stall</name>
    </Prop>
    <Prop>
      <tags/>
      <templates>
        <Entry>
          <name>Data/Props/1AD/Oven/1AD Oven.template</name>
        </Entry>
      </templates>
      <parts>
        <ModelPart>
          <slot>0</slot>
          <name>Data/Props/1AD/Oven/1AD Oven.part</name>
        </ModelPart>
      </parts>
      <skeleton>1AD_Bone.CSF</skeleton>
      <meshes>
        <string>Oven.CMF</string>
      </meshes>
      <animations/>
      <name>1AD/Oven</name>
    </Prop>
    <Prop>
      <tags/>
      <templates>
        <Entry>
          <name>Data/Props/1AD/Cup_01/1AD Cup 01.template</name>
        </Entry>
      </templates>
      <parts>
        <ModelPart>
          <slot>0</slot>
          <name>Data/Props/1AD/Cup_01/1AD Cup 01.part</name>
        </ModelPart>
      </parts>
      <skeleton>1AD_Bone.CSF</skeleton>
      <meshes>
        <string>Cup_01.CMF</string>
      </meshes>
      <animations/>
      <name>1AD/Cup_01</name>
    </Prop>
    <Prop>
      <tags/>
      <templates>
        <Entry>
          <name>Data/Props/1AD/Bowl_01/1AD Bowl 01.template</name>
        </Entry>
      </templates>
      <parts>
        <ModelPart>
          <slot>0</slot>
          <name>Data/Props/1AD/Bowl_01/1AD Bowl 01.part</name>
        </ModelPart>
      </parts>
      <skeleton>1AD_Bone.CSF</skeleton>
      <meshes>
        <string>Bowl_01.CMF</string>
      </meshes>
      <animations/>
      <name>1AD/Bowl_01</name>
    </Prop>
    <Prop>
      <tags/>
      <templates>
        <Entry>
          <name>Data/Props/1AD/Bowl_02/1AD Bowl 02.template</name>
        </Entry>
      </templates>
      <parts>
        <ModelPart>
          <slot>0</slot>
          <name>Data/Props/1AD/Bowl_02/1AD Bowl 02.part</name>
        </ModelPart>
      </parts>
      <skeleton>1AD_Bone.CSF</skeleton>
      <meshes>
        <string>Bowl_02.CMF</string>
      </meshes>
      <animations/>
      <name>1AD/Bowl_02</name>
    </Prop>
    <Prop>
      <tags/>
      <templates>
        <Entry>
          <name>Data/Props/Stall_Boxes/1AD Stall Boxes.template</name>
        </Entry>
      </templates>
      <parts>
        <ModelPart>
          <slot>0</slot>
          <name>Data/Props/Stall_Boxes/Basket.part</name>
        </ModelPart>
        <ModelPart>
          <slot>0</slot>
          <name>Data/Props/Stall_Boxes/Baskets (stacked).part</name>
        </ModelPart>
        <ModelPart>
          <slot>0</slot>
          <name>Data/Props/Stall_Boxes/Tray.part</name>
        </ModelPart>
        <ModelPart>
          <slot>0</slot>
          <name>Data/Props/Stall_Boxes/Wooden Crate.part</name>
        </ModelPart>
      </parts>
      <skeleton>Stall_Boxes.CSF</skeleton>
      <meshes>
        <string>Basket.CMF</string>
        <string>Box.CMF</string>
        <string>Boxes.CMF</string>
        <string>Tray.CMF</string>
      </meshes>
      <animations/>
      <name>Stall_Boxes</name>
    </Prop>
    <Prop>
      <tags/>
      <templates>
        <Entry>
          <name>Data/Props/1AD/Kettle/1AD Kettle.template</name>
        </Entry>
      </templates>
      <parts>
        <ModelPart>
          <slot>0</slot>
          <name>Data/Props/1AD/Kettle/1AD Kettle.part</name>
        </ModelPart>
      </parts>
      <skeleton>1AD_Bone.CSF</skeleton>
      <meshes>
        <string>Kettle.CMF</string>
      </meshes>
      <animations/>
      <name>1AD/Kettle</name>
    </Prop>
    <Prop>
      <tags/>
      <templates>
        <Entry>
          <name>Data/Props/1AD/House/1AD House 01.template</name>
        </Entry>
      </templates>
      <parts>
        <ModelPart>
          <slot>0</slot>
          <name>Data/Props/1AD/House/1AD House Door Open.part</name>
        </ModelPart>
        <ModelPart>
          <slot>0</slot>
          <name>Data/Props/1AD/House/1AD House Door Shut.part</name>
        </ModelPart>
        <ModelPart>
          <slot>0</slot>
          <name>Data/Props/1AD/House/1AD House Exterior Only.part</name>
        </ModelPart>
        <ModelPart>
          <slot>0</slot>
          <name>Data/Props/1AD/House/1AD House Interior Only.part</name>
        </ModelPart>
      </parts>
      <skeleton>1AD_House.CSF</skeleton>
      <meshes>
        <string>House_Door_Open.CMF</string>
        <string>House_Door_Shut.CMF</string>
        <string>House_Exterior.CMF</string>
        <string>House_Interior.CMF</string>
      </meshes>
      <animations/>
      <name>1AD/House</name>
    </Prop>
    <Prop>
      <tags/>
      <templates>
        <Entry>
          <name>Data/Props/Furnishings/Furniture/OldTable/Old Table.template</name>
        </Entry>
      </templates>
      <parts/>
      <skeleton>oldtable_bone.CSF</skeleton>
      <meshes>
        <string>oldtable_mesh.CMF</string>
      </meshes>
      <animations/>
      <name>Furnishings/Furniture/OldTable</name>
    </Prop>
    <Prop>
      <tags/>
      <templates>
        <Entry>
          <name>Data/Props/Flora/Shrubs/FigTree/Fig Tree.template</name>
        </Entry>
      </templates>
      <parts/>
      <skeleton>figtree_skel.CSF</skeleton>
      <meshes>
        <string>figtree_mesh.CMF</string>
      </meshes>
      <animations/>
      <name>Flora/Shrubs/FigTree</name>
    </Prop>
    <Prop>
      <tags/>
      <templates>
        <Entry>
          <name>Data/Props/1AD/Plate_01/1AD Plate 01.template</name>
        </Entry>
      </templates>
      <parts>
        <ModelPart>
          <slot>0</slot>
          <name>Data/Props/1AD/Plate_01/1AD Plate 01.part</name>
        </ModelPart>
      </parts>
      <skeleton>1AD_Bone.CSF</skeleton>
      <meshes>
        <string>Plate_01.CMF</string>
      </meshes>
      <animations/>
      <name>1AD/Plate_01</name>
    </Prop>
    <Prop>
      <tags/>
      <templates>
        <Entry>
          <name>Data/Props/1AD/Jar_02/1AD Jar 02.template</name>
        </Entry>
      </templates>
      <parts>
        <ModelPart>
          <slot>0</slot>
          <name>Data/Props/1AD/Jar_02/1AD Jar 02.part</name>
        </ModelPart>
      </parts>
      <skeleton>1AD_Bone.CSF</skeleton>
      <meshes>
        <string>Jar_02.CMF</string>
      </meshes>
      <animations/>
      <name>1AD/Jar_02</name>
    </Prop>
    <Prop>
      <tags/>
      <templates>
        <Entry>
          <name>Data/Props/1AD/Jar_01/1AD Jar 01.template</name>
        </Entry>
      </templates>
      <parts>
        <ModelPart>
          <slot>0</slot>
          <name>Data/Props/1AD/Jar_01/1AD Jar 01.part</name>
        </ModelPart>
      </parts>
      <skeleton>1AD_Bone.CSF</skeleton>
      <meshes>
        <string>Jar_01.CMF</string>
      </meshes>
      <animations/>
      <name>1AD/Jar_01</name>
    </Prop>
    <Prop>
      <tags/>
      <templates>
        <Entry>
          <name>Data/Props/1AD/Jar_03/1AD Jar 03.template</name>
        </Entry>
      </templates>
      <parts>
        <ModelPart>
          <slot>0</slot>
          <name>Data/Props/1AD/Jar_03/1AD Jar 03.part</name>
        </ModelPart>
      </parts>
      <skeleton>1AD_Bone.CSF</skeleton>
      <meshes>
        <string>Jar_03.CMF</string>
      </meshes>
      <animations/>
      <name>1AD/Jar_03</name>
    </Prop>
  </propModels>
  <bodyModels>
    <Body>
      <templates/>
      <parts>
        <BodyPart>
          <partsCovered class='enum-set' enum-type='mscope.things.factory.BodyPart$Category'>WIG</partsCovered>
          <instanceClass>mscope.things.factory.BodyPart</instanceClass>
          <name>Data/Puppets/Female01/1AD Headwear 01.bodypart</name>
        </BodyPart>
        <BodyPart>
          <partsCovered class='enum-set' enum-type='mscope.things.factory.BodyPart$Category'>BODY</partsCovered>
          <instanceClass>mscope.things.factory.BodyPart</instanceClass>
          <name>Data/Puppets/Female01/1AD Robes 01.bodypart</name>
        </BodyPart>
      </parts>
      <meshes>
        <string>Costumes/1AD_Robes_01/1AD_Robes_01.CMF</string>
        <string>Hats/1AD_Headwear_01/1AD_Headwear_01.CMF</string>
      </meshes>
      <animations/>
      <name>Female01</name>
    </Body>
    <Body>
      <templates/>
      <parts>
        <BodyPart>
          <partsCovered class='enum-set' enum-type='mscope.things.factory.BodyPart$Category'>WIG</partsCovered>
          <instanceClass>mscope.things.factory.BodyPart</instanceClass>
          <name>Data/Puppets/Male01/1AD Headwear 01.bodypart</name>
        </BodyPart>
        <BodyPart>
          <partsCovered class='enum-set' enum-type='mscope.things.factory.BodyPart$Category'>BODY</partsCovered>
          <instanceClass>mscope.things.factory.BodyPart</instanceClass>
          <name>Data/Puppets/Male01/1AD Robes 01.bodypart</name>
        </BodyPart>
      </parts>
      <meshes>
        <string>Costumes/1AD_Robes_01/1AD_Robes_01.CMF</string>
        <string>Hats/1AD_Headwear_01/1AD_Headwear_01.CMF</string>
      </meshes>
      <animations/>
      <name>Male01</name>
    </Body>
  </bodyModels>
</AssetManifest>
";

        private const string AssetManifestFileText1 =
        @"<AssetManifest>
  <propModels/>
  <bodyModels>
    <Body>
      <templates/>
      <parts>
        <BodyPart>
          <partsCovered class='enum-set' enum-type='mscope.things.factory.BodyPart$Category'>BODY</partsCovered>
          <instanceClass>mscope.things.factory.BodyPart</instanceClass>
          <name>Data/Puppets/Female01/Figleaf Female.bodypart</name>
        </BodyPart>
        <BodyPart>
          <partsCovered class='enum-set' enum-type='mscope.things.factory.BodyPart$Category'>BODY</partsCovered>
          <instanceClass>mscope.things.factory.BodyPart$MorphPart</instanceClass>
          <name>Data/Puppets/Female01/Naked Female.bodypart</name>
        </BodyPart>
      </parts>
      <meshes>
        <string>Costumes/Naked_Full/Figleaf.CMF</string>
        <string>Costumes/Naked_Full/Naked_01.CMF</string>
        <string>Costumes/Naked_Full/Naked_02_Bigger.CMF</string>
        <string>Costumes/Naked_Full/Naked_03_Smaller.CMF</string>
      </meshes>
      <animations>
        <string>Animations/Sex/Blowjob_01/Blowjob01_Loop1_A_Male01.CAF</string>
        <string>Animations/Sex/Blowjob_01/Blowjob01_Loop2_A_Male01.CAF</string>
        <string>Animations/Sex/Blowjob_01/Blowjob01_Position2_A_Male01.CAF</string>
        <string>Animations/Sex/Blowjob_01/Blowjob01_Position2to1_A_Male01.CAF</string>
        <string>Animations/Sex/Blowjob_01/Blowjob01_Start_A_Male01.CAF</string>
        <string>Animations/Sex/Blowjob_01/Blowjob01_Stop_A_Male01.CAF</string>
        <string>Animations/Sex/Doggy_Floor/Doggy_Floor_Start_B_Male01.CAF</string>
        <string>Animations/Sex/Doggy_Floor/Doggy_Floor_Stop_B_Male01.CAF</string>
        <string>Animations/Sex/Doggy_Floor/Doggy_Floor_Thrust1_B_Male01.CAF</string>
        <string>Animations/Sex/Doggy_Floor/Doggy_Floor_Thrust2_B_Male01.CAF</string>
        <string>Animations/Sex/Doggy_Floor/Doggy_Floor_Thrust3_B_Male01.CAF</string>
        <string>Animations/Sex/Table_Over/Table_Over_Loop1_B_Male01.CAF</string>
        <string>Animations/Sex/Table_Over/Table_Over_Start_B_Male01.CAF</string>
        <string>Animations/Sex/Table_Over/Table_Over_Stop_B_Male01.CAF</string>
        <string>Animations/Sex/Table_Sat/Table_Sat_Loop1_B_Male01.CAF</string>
        <string>Animations/Sex/Table_Sat/Table_Sat_Start_B_Male01.CAF</string>
        <string>Animations/Sex/Table_Sat/Table_Sat_Stop_B_Male01.CAF</string>
      </animations>
      <name>Female01</name>
    </Body>
    <Body>
      <templates/>
      <parts>
        <BodyPart>
          <partsCovered class='enum-set' enum-type='mscope.things.factory.BodyPart$Category'>BODY</partsCovered>
          <instanceClass>mscope.things.factory.BodyPart</instanceClass>
          <name>Data/Puppets/Male01/Figleaf Male.bodypart</name>
        </BodyPart>
        <BodyPart>
          <partsCovered class='enum-set' enum-type='mscope.things.factory.BodyPart$Category'>BODY</partsCovered>
          <instanceClass>mscope.things.factory.BodyPart$MorphPart</instanceClass>
          <name>Data/Puppets/Male01/Naked Male.bodypart</name>
        </BodyPart>
      </parts>
      <meshes>
        <string>Costumes/Naked_Full/FigLeaf.CMF</string>
        <string>Costumes/Naked_Full/Naked_Erection.CMF</string>
        <string>Costumes/Naked_Full/Naked_Flacid.CMF</string>
      </meshes>
      <animations>
        <string>Animations/Sex/Blowjob_01/Blowjob01_Loop1_A_Male01.CAF</string>
        <string>Animations/Sex/Blowjob_01/Blowjob01_Loop1_B_Female01.CAF</string>
        <string>Animations/Sex/Blowjob_01/Blowjob01_Loop1_B_Male01.CAF</string>
        <string>Animations/Sex/Blowjob_01/Blowjob01_Loop2_A_Male01.CAF</string>
        <string>Animations/Sex/Blowjob_01/Blowjob01_Loop2_B_Female01.CAF</string>
        <string>Animations/Sex/Blowjob_01/Blowjob01_Loop2_B_Male01.CAF</string>
        <string>Animations/Sex/Blowjob_01/Blowjob01_Position2_A_Male01.CAF</string>
        <string>Animations/Sex/Blowjob_01/Blowjob01_Position2_B_Female01.CAF</string>
        <string>Animations/Sex/Blowjob_01/Blowjob01_Position2_B_Male01.CAF</string>
        <string>Animations/Sex/Blowjob_01/Blowjob01_Position2to1_A_Male01.CAF</string>
        <string>Animations/Sex/Blowjob_01/Blowjob01_Position2to1_B_Female01.CAF</string>
        <string>Animations/Sex/Blowjob_01/Blowjob01_Position2to1_B_Male01.CAF</string>
        <string>Animations/Sex/Blowjob_01/Blowjob01_Start_A_Male01.CAF</string>
        <string>Animations/Sex/Blowjob_01/Blowjob01_Start_B_Female01.CAF</string>
        <string>Animations/Sex/Blowjob_01/Blowjob01_Start_B_Male01.CAF</string>
        <string>Animations/Sex/Blowjob_01/Blowjob01_Stop_A_Male01.CAF</string>
        <string>Animations/Sex/Blowjob_01/Blowjob01_Stop_B_Female01.CAF</string>
        <string>Animations/Sex/Blowjob_01/Blowjob01_Stop_B_Male01.CAF</string>
        <string>Animations/Sex/Doggy_Floor/Doggy_Floor_Start_A_Female01.CAF</string>
        <string>Animations/Sex/Doggy_Floor/Doggy_Floor_Start_A_Male01.CAF</string>
        <string>Animations/Sex/Doggy_Floor/Doggy_Floor_Start_B_Male01.CAF</string>
        <string>Animations/Sex/Doggy_Floor/Doggy_Floor_Stop_A_Female01.CAF</string>
        <string>Animations/Sex/Doggy_Floor/Doggy_Floor_Stop_A_Male01.CAF</string>
        <string>Animations/Sex/Doggy_Floor/Doggy_Floor_Stop_B_Male01.CAF</string>
        <string>Animations/Sex/Doggy_Floor/Doggy_Floor_Thrust1_A_Female01.CAF</string>
        <string>Animations/Sex/Doggy_Floor/Doggy_Floor_Thrust1_A_Male01.CAF</string>
        <string>Animations/Sex/Doggy_Floor/Doggy_Floor_Thrust1_B_Male01.CAF</string>
        <string>Animations/Sex/Doggy_Floor/Doggy_Floor_Thrust2_A_Female01.CAF</string>
        <string>Animations/Sex/Doggy_Floor/Doggy_Floor_Thrust2_A_Male01.CAF</string>
        <string>Animations/Sex/Doggy_Floor/Doggy_Floor_Thrust2_B_Male01.CAF</string>
        <string>Animations/Sex/Doggy_Floor/Doggy_Floor_Thrust3_A_Female01.CAF</string>
        <string>Animations/Sex/Doggy_Floor/Doggy_Floor_Thrust3_A_Male01.CAF</string>
        <string>Animations/Sex/Doggy_Floor/Doggy_Floor_Thrust3_B_Male01.CAF</string>
        <string>Animations/Sex/Table_Over/Table_Over_Loop1_A_Female01.CAF</string>
        <string>Animations/Sex/Table_Over/Table_Over_Loop1_A_Male01.CAF</string>
        <string>Animations/Sex/Table_Over/Table_Over_Loop1_B_Male01.CAF</string>
        <string>Animations/Sex/Table_Over/Table_Over_Start_A_Female01.CAF</string>
        <string>Animations/Sex/Table_Over/Table_Over_Start_A_Male01.CAF</string>
        <string>Animations/Sex/Table_Over/Table_Over_Start_B_Male01.CAF</string>
        <string>Animations/Sex/Table_Over/Table_Over_Stop_A_Female01.CAF</string>
        <string>Animations/Sex/Table_Over/Table_Over_Stop_A_Male01.CAF</string>
        <string>Animations/Sex/Table_Over/Table_Over_Stop_B_Male01.CAF</string>
        <string>Animations/Sex/Table_Sat/Table_Sat_Loop1_A_Female01.CAF</string>
        <string>Animations/Sex/Table_Sat/Table_Sat_Loop1_A_Male01.CAF</string>
        <string>Animations/Sex/Table_Sat/Table_Sat_Loop1_B_Male01.CAF</string>
        <string>Animations/Sex/Table_Sat/Table_Sat_Start_A_Female01.CAF</string>
        <string>Animations/Sex/Table_Sat/Table_Sat_Start_A_Male01.CAF</string>
        <string>Animations/Sex/Table_Sat/Table_Sat_Start_B_Male01.CAF</string>
        <string>Animations/Sex/Table_Sat/Table_Sat_Stop_A_Female01.CAF</string>
        <string>Animations/Sex/Table_Sat/Table_Sat_Stop_A_Male01.CAF</string>
        <string>Animations/Sex/Table_Sat/Table_Sat_Stop_B_Male01.CAF</string>
      </animations>
      <name>Male01</name>
    </Body>
  </bodyModels>
</AssetManifest>";
        
    }
}
