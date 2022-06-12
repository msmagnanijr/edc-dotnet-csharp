using Domain.Model.Entities;
using Domain.Model.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[Authorize]
[Route("api/[controller]")]
[ApiController]
public class ReviewsController : ControllerBase
{
    private readonly IReviewService _domainService;
    private readonly ICommentService _domainServiceComment;
    public ReviewsController(IReviewService domainService, ICommentService domainServiceComment)
    {
        _domainService = domainService;
        _domainServiceComment = domainServiceComment;
    }

    [HttpGet]
    [Route("getreviews")]
    public async Task<IEnumerable<ReviewEntity>> GetReviews()
    {
        return await _domainService.GetAllAsync();
    }

    [HttpGet]
    [Route("getreview/{id}")]
    public async Task<ActionResult<ReviewEntity>> GetReview(int id)
    {
        var review = await _domainService.GetByIdAsync(id);

        if (review == null)
        {
            return NotFound();
        }

        return review;
    }

    [HttpPost]
    [Route("createreview")]
    public async Task<ActionResult<ReviewEntity>> PostReview(ReviewEntity review)
    {
        await _domainService.InsertAsync(review);
        return CreatedAtAction("GetReview", new { id = review.Id }, review);
    }

    [HttpPut]
    [Route("updatereview")]
    public async Task<ActionResult<ReviewEntity>> PutReview(ReviewEntity review)
    {
        await _domainService.UpdateAsync(review);
        return CreatedAtAction("GetReview", new { id = review.Id }, review);
    }

    [HttpDelete]
    [Route("removereview/{id}")]
    public async Task<IActionResult> DeleteReview(int id)
    {
        var review = await _domainService.GetByIdAsync(id);
        await _domainService.DeleteAsync(review);
        return NoContent();
    }

    [HttpGet]
    [Route("getcomments/{id}")]
    public async Task<IEnumerable<CommentEntity>> GetComments(int id)
    {
        return await _domainServiceComment.GetAllByReviewId(id);
    }

    [HttpGet]
    [Route("getcomment/{id}")]
    public async Task<CommentEntity> GetComment(int id)
    {
        return await _domainServiceComment.GetByIdAsync(id);
    }

    [HttpGet]
    [Route("getallcomments")]
    public async Task<IEnumerable<CommentEntity>> GetAllComments()
    {
        return await _domainServiceComment.GetAllAsync();
    }

    [HttpPost]
    [Route("createcomment")]
    public async Task<ActionResult<CommentEntity>> PostComment(CommentEntity comment)
    {
        await _domainServiceComment.InsertAsync(comment);
        return NoContent();
    }

    [HttpPut]
    [Route("updatecomment")]
    public async Task<ActionResult<CommentEntity>> PutComment(CommentEntity comment)
    {
        await _domainServiceComment.UpdateAsync(comment);
        return NoContent();

    }
}