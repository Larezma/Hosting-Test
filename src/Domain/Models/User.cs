using System;
using System.Collections.Generic;

namespace Domain.Models;

public partial class User
{
    public int UserId { get; set; }

    public string UserName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;

    public int? RoleUser { get; set; }

    public string? UserImg { get; set; }

    public DateTime? CreateAt { get; set; }

    public DateTime? UpdateAt { get; set; }

    public string PhoneNumber { get; set; } = null!;

    public string? AboutMe { get; set; }

    public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();

    public virtual ICollection<Friend> FriendUserId1Navigations { get; set; } = new List<Friend>();

    public virtual ICollection<Friend> FriendUserId2Navigations { get; set; } = new List<Friend>();

    public virtual Group? Group { get; set; }

    public virtual ICollection<GroupMember> GroupMembers { get; set; } = new List<GroupMember>();

    public virtual ICollection<MessageUser> MessageUserReceivers { get; set; } = new List<MessageUser>();

    public virtual ICollection<MessageUser> MessageUserSenders { get; set; } = new List<MessageUser>();

    public virtual ICollection<PhotoUser> PhotoUsers { get; set; } = new List<PhotoUser>();

    public virtual ICollection<Publication> Publications { get; set; } = new List<Publication>();

    public virtual ICollection<UserNutrition> UserNutritions { get; set; } = new List<UserNutrition>();

    public virtual ICollection<UserToAchievement> UserToAchievements { get; set; } = new List<UserToAchievement>();

    public virtual ICollection<UserToDialog> UserToDialogs { get; set; } = new List<UserToDialog>();

    public virtual ICollection<UserToRule> UserToRules { get; set; } = new List<UserToRule>();

    public virtual ICollection<UserTraining> UserTrainings { get; set; } = new List<UserTraining>();
}