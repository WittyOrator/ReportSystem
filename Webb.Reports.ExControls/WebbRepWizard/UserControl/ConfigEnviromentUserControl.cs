using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace WizardEnvironmentSetting
{
    public partial class ConfigEnviromentUserControl : UserControl
    {
        public EnviromentUserControlInfo ParentUserControlInfo = null;

        public ConfigEnviromentUserControl()
        {
            InitializeComponent();
        }
        public virtual void SetData(object i_Data)
        {
        }
        public virtual bool UpdateData(object i_Data)
        {
            return true;
        }
        public void SetStepText(int index,int count)
        {
            this.lblStep.Text = string.Format("Step:{0}/{1}", index, count);
        }
        public void DrawBackImage(Graphics g, Image image)
        {
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighSpeed;

            g.DrawImage(image, this.ClientRectangle);

        }



    }

    public class EnviromentUserControlInfo
    {
        #region Auto Constructor By Macro 2010-10-18 13:33:17
        public EnviromentUserControlInfo()
        {
            _StepIndex = 0;
            _ConfigControl = null;
        }

        public EnviromentUserControlInfo(ConfigEnviromentUserControl p_ConfigControl, int p_StepIndex)
        {
            _StepIndex = p_StepIndex;
            _ConfigControl = p_ConfigControl;
        }
        #endregion


        protected int _StepIndex = -1;
        protected ConfigEnviromentUserControl _ConfigControl = null;

        public int StepIndex
        {
            get
            {
                return _StepIndex;
            }
            set
            {
                _StepIndex = value;
            }
        }

        public WizardEnvironmentSetting.ConfigEnviromentUserControl ConfigControl
        {
            get
            {
                return _ConfigControl;
            }
            set
            {
                _ConfigControl = value;
            }
        }

        public void SetData(object i_Data)
        {
            this._ConfigControl.SetData(i_Data);
        }
        public bool UpdateData(object i_Data)
        {
            return this._ConfigControl.UpdateData(i_Data);
        }
    }
 
}
