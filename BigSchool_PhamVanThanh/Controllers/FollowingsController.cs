﻿using BigSchool_PhamVanThanh.DTOs;
using BigSchool_PhamVanThanh.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BigSchool_PhamVanThanh.Controllers
{
    public class FollowingsController : ApiController
    {
        private readonly ApplicationDbContext _dbContext;
        public FollowingsController()
        {
            _dbContext = new ApplicationDbContext();
        }
        [HttpPost]
        public IHttpActionResult Follow(FollowingDto followingDto)
        {
            var userId = User.Identity.GetUserId();
            if (_dbContext.Followings.Any(f => f.FollowerId == userId && f.FolloweeId == followingDto.FolloweeId))
                return BadRequest("Following already exists!");
            var folowing = new Following
            {
                FollowerId = userId,
                FolloweeId = followingDto.FolloweeId
            };

            _dbContext.Followings.Add(folowing);
            _dbContext.SaveChanges();

            return Ok();
        }
        [HttpDelete]
        public IHttpActionResult DeleteFollow(string Id)
        {
            var userId = User.Identity.GetUserId();
            var following = _dbContext.Followings.SingleOrDefault(f => f.FollowerId == userId && f.FolloweeId == Id);
            if (following == null)
            {
                return NotFound();
            }
            _dbContext.Followings.Remove(following);
            _dbContext.SaveChanges();
            return Ok(Id);
        }
    }
}
