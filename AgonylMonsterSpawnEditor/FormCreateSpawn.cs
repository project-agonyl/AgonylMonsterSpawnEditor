using System;
using System.Linq;
using System.Windows.Forms;

namespace AgonylMonsterSpawnEditor
{
    public partial class FormCreateSpawn : Form
    {
        private FormSpawnEditor _parentForm;

        public FormCreateSpawn(FormSpawnEditor parent)
        {
            InitializeComponent();
            this._parentForm = parent;
        }

        private void FormCreateSpawn_Load(object sender, EventArgs e)
        {
            this.npcList.DisplayMember = "Name";
            this.npcList.ValueMember = "Id";
            this.npcList.DataSource = Utils.MonsterList.Values.ToList();
        }

        private void AddButton_Click(object sender, EventArgs e)
        {
            if (!byte.TryParse(this.X.Text.Trim(), out _))
            {
                _ = MessageBox.Show("X has to be a positive number (max 255)", "Agonyl Monster Spawn Editor", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!byte.TryParse(this.Y.Text.Trim(), out _))
            {
                _ = MessageBox.Show("Y has to be a positive number (max 255)", "Agonyl Monster Spawn Editor", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!byte.TryParse(this.Orientation.Text.Trim(), out _))
            {
                _ = MessageBox.Show("Orientation has to be a positive number (max 255)", "Agonyl Monster Spawn Editor", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!byte.TryParse(this.SpawnStep.Text.Trim(), out _))
            {
                _ = MessageBox.Show("Spawn step has to be a positive number (max 255)", "Agonyl Monster Spawn Editor", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            this._parentForm.A3NpcDataBound.Add(new A3NPCData()
            {
                Id = Convert.ToUInt32(this.npcList.SelectedValue),
                Name = Utils.MonsterList.ContainsKey((uint)this.npcList.SelectedValue) ? Utils.MonsterList[(uint)this.npcList.SelectedValue].Name : "Unknown Monster",
                LocationX = Convert.ToByte(this.X.Text),
                LocationY = Convert.ToByte(this.Y.Text),
                Orientation = Convert.ToByte(this.Orientation.Text),
                SpawnStep = Convert.ToByte(this.SpawnStep.Text),
            });

            this.Close();
        }
    }
}
