using System;
using System.ComponentModel;
using System.IO;
using System.Windows.Forms;

namespace AgonylMonsterSpawnEditor
{
    public partial class FormSpawnEditor : Form
    {
        public string NdtFile;

        private byte[] _fileData;
        public BindingList<A3NPCData> A3NpcDataBound = new BindingList<A3NPCData>();

        public FormSpawnEditor()
        {
            InitializeComponent();
        }

        private void FormSpawnEditor_Load(object sender, EventArgs e)
        {
            this.Text = "Spawn Editor - " + Path.GetFileName(this.NdtFile);
            this._fileData = File.ReadAllBytes(this.NdtFile);
            this.SaveFileDialog.FileName = Path.GetFileName(this.NdtFile);
            this.SaveFileDialog.InitialDirectory = new FileInfo(this.NdtFile).Directory.Name;
            this.dataGridView.AutoGenerateColumns = false;
            this.dataGridView.Columns.Add(new DataGridViewTextBoxColumn()
            {
                DataPropertyName = "Id",
                Name = "Entity ID",
                Width = 100,
            });
            this.dataGridView.Columns.Add(new DataGridViewTextBoxColumn()
            {
                DataPropertyName = "Name",
                Name = "Entity Name",
                Width = 150,
            });
            this.dataGridView.Columns.Add(new DataGridViewTextBoxColumn()
            {
                DataPropertyName = "LocationX",
                Name = "X",
                Width = 50,
            });
            this.dataGridView.Columns.Add(new DataGridViewTextBoxColumn()
            {
                DataPropertyName = "LocationY",
                Name = "Y",
                Width = 50,
            });
            this.dataGridView.Columns.Add(new DataGridViewTextBoxColumn()
            {
                DataPropertyName = "Orientation",
                Name = "Orientation",
                Width = 100,
            });
            this.dataGridView.Columns.Add(new DataGridViewTextBoxColumn()
            {
                DataPropertyName = "SpawnStep",
                Name = "Spawn Step",
                Width = 100,
            });
            this.dataGridView.DataSource = this.A3NpcDataBound;
            this._fileData = File.ReadAllBytes(this.NdtFile);
            for (var i = 0; i < this._fileData.Length; i += 8)
            {
                var npcId = Utils.BytesToUInt16(Utils.SkipAndTakeLinqShim(ref this._fileData, 2, i));
                string name;
                if (npcId < 1000)
                {
                    name = Utils.MonsterList.ContainsKey(npcId) ? Utils.MonsterList[npcId].Name : "Unknown Monster";
                }
                else
                {
                    name = Utils.NpcList.ContainsKey(npcId) ? Utils.NpcList[npcId].Name : "Unknown NPC";
                }

                this.A3NpcDataBound.Add(new A3NPCData()
                {
                    Id = npcId,
                    Name = name,
                    LocationX = this._fileData[i + 2],
                    LocationY = this._fileData[i + 3],
                    Orientation = this._fileData[i + 6],
                    SpawnStep = this._fileData[i + 7],
                });
            }
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            if (this.A3NpcDataBound.Count == 0)
            {
                _ = MessageBox.Show("Spawn list is empty", "Agonyl Monster Spawn Editor", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (this.SaveFileDialog.ShowDialog() != DialogResult.Cancel)
            {
                var dataBuilder = new BinaryDataBuilder();
                foreach (var item in this.A3NpcDataBound)
                {
                    dataBuilder.PutBytes(BitConverter.GetBytes((ushort)item.Id));
                    dataBuilder.PutByte(item.LocationX);
                    dataBuilder.PutByte(item.LocationY);
                    dataBuilder.PutByte(0x00);
                    dataBuilder.PutByte(0x00);
                    dataBuilder.PutByte(item.Orientation);
                    dataBuilder.PutByte(item.SpawnStep);
                }

                File.WriteAllBytes(this.SaveFileDialog.FileName, dataBuilder.GetBuffer());
                _ = MessageBox.Show("Saved the file " + Path.GetFileName(this.SaveFileDialog.FileName), "Agonyl Monster Spawn Editor", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void addButton_Click(object sender, EventArgs e)
        {
            var form = new FormCreateSpawn(this);
            form.ShowDialog();
        }
    }
}
