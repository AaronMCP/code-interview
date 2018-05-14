using System;
using System.Collections.Generic;
using System.Text;

namespace CommonGlobalSettings
{
    #region Validation decorator
    /// <summary>
    /// definition of class ValidationDecorator.
    /// it is the derived class of Validation
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ValidationDecorator : Validation
    {
        protected Validation m_obj = null;

        public Validation ValidationObj
        {
            get { return m_obj; }
            set { m_obj = value; }
        }

        /// <summary>
        ///  constructor for ValidationDecorator
        /// </summary>
        public ValidationDecorator()
        { }

        /// <summary>
        ///  constructor for ValidationDecorator
        /// </summary>
        public ValidationDecorator(Validation obj)
        {
            this.m_obj = obj;
        }
        public void SetValidationObj(Validation obj)
        {
            m_obj = obj;
        }

        /// <summary>
        ///  Implement for Validate method of ValidationDecorator
        /// </summary>
        /// <returns></returns>
        public virtual bool Validate()
        {
            return m_obj.Validate();
        }
    }
    #endregion
}
