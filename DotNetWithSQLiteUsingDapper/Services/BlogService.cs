﻿using Dapper;
using DotNetWithSQLiteUsingDapper.DbServices;
using DotNetWithSQLiteUsingDapper.Model;
using DotNetWithSQLiteUsingDapper.Queries;

namespace DotNetWithSQLiteUsingDapper.Services
{
    public interface IBlogService
    {
        Task<IEnumerable<BlogModel>> GetAll();
        //Task<BlogModel> GetById(int id);
        Task<ResponseModel> Create(BlogRequestModel model);
        Task<ResponseModel> Update(int id, BlogUpdateModel model);
        Task Delete(int id);
    }
    public class BlogService : IBlogService
    {
        private DataContext _context;
        public BlogService(DataContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<BlogModel>> GetAll()
        {
            using var connection = _context.CreateConnection();
            var sql = BlogQuery.GetAll;
            return await connection.QueryAsync<BlogModel>(sql);
        }

        public async Task<ResponseModel> Create(BlogRequestModel reqModel)
        {
            ResponseModel model = new ResponseModel();
            using var connection = _context.CreateConnection();
            var sql = BlogQuery.Insert;
            var res = await connection.ExecuteAsync(sql, reqModel);
            model.Message = res > 0 ? "Create Successful" : "Create Failed";
            return model;
        }
        public async Task<ResponseModel> Update(int id, BlogUpdateModel reqModel)
        {
            ResponseModel model = new ResponseModel();
            using var connection = _context.CreateConnection();
            var sql = BlogQuery.Update;
            var res = await connection.ExecuteAsync(sql, reqModel);
            model.Message = res > 0 ? "Update Successful" : "Update Failed";
            return model;
        }
        public async Task Delete(int @BlogId)
        {
            using var connection = _context.CreateConnection();
            var sql = BlogQuery.Delete;
            await connection.ExecuteAsync(sql, new { @BlogId });
        }
    }
}
