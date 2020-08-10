using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Windows.Forms;

namespace AgonylMonsterSpawnEditor
{
    public partial class FormMain : Form
    {
        private Queue<A3MapData> _a3MapData = new Queue<A3MapData>();
        private BindingList<A3MapData> _a3MapDataBound = new BindingList<A3MapData>();

        public FormMain()
        {
            InitializeComponent();
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            if (!File.Exists(Utils.GetMyDirectory() + Path.DirectorySeparatorChar + "NPC.txt"))
            {
                _ = MessageBox.Show("Please place NPC.txt in same folder as this application", "Agonyl Monster Spawn Editor", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }

            if (!File.Exists(Utils.GetMyDirectory() + Path.DirectorySeparatorChar + "MC.txt"))
            {
                _ = MessageBox.Show("Please place MC.txt in same folder as this application", "Agonyl Monster Spawn Editor", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }

            if (!File.Exists(Utils.GetMyDirectory() + Path.DirectorySeparatorChar + "MON.txt"))
            {
                _ = MessageBox.Show("Please place MON.txt in same folder as this application", "Agonyl Monster Spawn Editor", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }

            Utils.LoadNpcData();
            Utils.LoadMapData();
            Utils.LoadMonsterData();
            this.dataGridView.AutoGenerateColumns = false;
            this.dataGridView.Columns.Add(new DataGridViewTextBoxColumn()
            {
                DataPropertyName = "Id",
                Name = "Map ID",
                Width = 150,
            });
            this.dataGridView.Columns.Add(new DataGridViewTextBoxColumn()
            {
                DataPropertyName = "Name",
                Name = "Map Name",
                Width = 150,
            });
            this.dataGridView.Columns.Add(new DataGridViewTextBoxColumn()
            {
                DataPropertyName = "MonsterCount",
                Name = "Monsters",
                Width = 150,
            });
            this.dataGridView.Columns.Add(new DataGridViewTextBoxColumn()
            {
                DataPropertyName = "NpcCount",
                Name = "NPCs",
                Width = 150,
            });
            this.dataGridView.DataSource = this._a3MapDataBound;
            this.ReloadDataButton.Enabled = false;
        }

        private void ChooseMapFolderButton_Click(object sender, EventArgs e)
        {
            if (this.FolderBrowser.ShowDialog() == DialogResult.OK)
            {
                this.CurrentMapFolderLabel.Text = this.FolderBrowser.SelectedPath;
                this.MapDataLoaderBgWorker.RunWorkerAsync();
            }
        }

        private void ReloadDataButton_Click(object sender, EventArgs e)
        {
            if (this.MapDataLoaderBgWorker.IsBusy)
            {
                _ = MessageBox.Show("Map loader is busy!", "Agonyl Monster Spawn Editor", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            this._a3MapDataBound.Clear();
            this._a3MapData.Clear();
            this.MapDataLoaderBgWorker.RunWorkerAsync();
            this.ReloadDataButton.Enabled = false;
            this.ChooseMapFolderButton.Enabled = false;
        }

        private void MapDataLoaderBgWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            if (!Directory.Exists(this.CurrentMapFolderLabel.Text))
            {
                return;
            }

            foreach (var file in Directory.GetFiles(this.CurrentMapFolderLabel.Text, "*.n_ndt"))
            {
                try
                {
                    var mapId = Convert.ToUInt16(Path.GetFileNameWithoutExtension(file));
                    var fileBytes = File.ReadAllBytes(file);
                    ushort monsterCount = 0;
                    ushort npcCount = 0;
                    for (var i = 0; i < fileBytes.Length; i += 8)
                    {
                        var id = Utils.BytesToUInt16(Utils.SkipAndTakeLinqShim(ref fileBytes, 2, i));
                        if (id >= 1000)
                        {
                            npcCount++;
                        }
                        else
                        {
                            monsterCount++;
                        }
                    }

                    var mapData = new A3MapData()
                    {
                        Id = mapId,
                        Name = Utils.MapList.ContainsKey(mapId) ? Utils.MapList[mapId].Name : mapId.ToString(),
                        MonsterCount = monsterCount,
                        NpcCount = npcCount,
                        File = file,
                    };
                    this._a3MapData.Enqueue(mapData);
                    this.MapDataLoaderBgWorker.ReportProgress(mapData.Id);
                }
                catch { }
            }
        }

        private void MapDataLoaderBgWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            if (this._a3MapData.Count != 0)
            {
                this._a3MapDataBound.Add(this._a3MapData.Dequeue());
            }
        }

        private void MapDataLoaderBgWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            this.dataGridView.Refresh();
            if (this._a3MapDataBound.Count == 0)
            {
                _ = MessageBox.Show("Could not find any n_ndt file", "Agonyl Monster Editor", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                this.FixEmptyCells();
                _ = MessageBox.Show("Loaded " + this._a3MapDataBound.Count + " n_ndt files", "Agonyl Monster Editor", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            this.ReloadDataButton.Enabled = true;
            this.ChooseMapFolderButton.Enabled = true;
        }

        private void dataGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            var dataIndexNo = this.dataGridView.Rows[e.RowIndex].Index;
            var editorForm = new FormSpawnEditor();
            editorForm.NdtFile = this._a3MapDataBound[dataIndexNo].File;
            editorForm.ShowDialog();
        }

        private void dataGridView_DragEnter(object sender, DragEventArgs e)
        {
            if (!this.MapDataLoaderBgWorker.IsBusy)
            {
                // Check if the Data format of the file(s) can be accepted
                // (we only accept file drops from Windows Explorer, etc.)
                if (e.Data.GetDataPresent(DataFormats.FileDrop))
                {
                    // modify the drag drop effects to Move
                    e.Effect = DragDropEffects.All;
                }
                else
                {
                    // no need for any drag drop effect
                    e.Effect = DragDropEffects.None;
                }
            }
        }

        private void dataGridView_DragDrop(object sender, DragEventArgs e)
        {
            if (!this.MapDataLoaderBgWorker.IsBusy)
            {
                // still check if the associated data from the file(s) can be used for this purpose
                if (e.Data.GetDataPresent(DataFormats.FileDrop))
                {
                    // Fetch the file(s) names with full path here to be processed
                    var fileList = (string[])e.Data.GetData(DataFormats.FileDrop);
                    if (Path.GetExtension(fileList[0]) == ".n_ndt")
                    {
                        var editorForm = new FormSpawnEditor();
                        editorForm.NdtFile = fileList[0];
                        editorForm.ShowDialog();
                    }
                }
            }
        }

        // This fix is needed as I cannot figure out why cell gets empty though data is available
        private void FixEmptyCells()
        {
            foreach (DataGridViewRow rw in this.dataGridView.Rows)
            {
                if (rw.Index >= this._a3MapDataBound.Count)
                {
                    break;
                }

                for (var i = 0; i < rw.Cells.Count; i++)
                {
                    if (rw.Cells[i].Value == null)
                    {
                        switch (i)
                        {
                            case 0:
                                rw.Cells[i].Value = this._a3MapDataBound[rw.Index].Name;
                                break;

                            case 1:
                                rw.Cells[i].Value = this._a3MapDataBound[rw.Index].MonsterCount;
                                break;

                            case 2:
                                rw.Cells[i].Value = this._a3MapDataBound[rw.Index].NpcCount;
                                break;
                        }
                    }
                }
            }
        }
    }
}
