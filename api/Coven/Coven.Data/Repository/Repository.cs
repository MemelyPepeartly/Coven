using Coven.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coven.Data.Repository
{
    public class Repository : IRepository
    {
        private readonly CovenContext CovenContext;

        public Repository(CovenContext _CovenContext)
        {
            CovenContext = _CovenContext ?? throw new ArgumentNullException(nameof(_CovenContext));
        }

        #region Create
        #endregion

        #region Read
        #endregion

        #region Update
        #endregion

        #region Delete
        #endregion
    }
}
