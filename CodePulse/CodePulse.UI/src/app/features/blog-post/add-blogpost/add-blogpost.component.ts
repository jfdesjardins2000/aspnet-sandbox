import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { DatePipe } from '@angular/common';
import { BlogPostService } from '../services/blog-post.service';
import { AddBlogPostModel } from '../models/add-blog-post.model';
import { Router } from '@angular/router';



@Component({
  selector: 'app-add-blogpost',
  standalone: true,
  imports: [FormsModule, DatePipe],
  templateUrl: './add-blogpost.component.html',
  styleUrl: './add-blogpost.component.css'
})
export class AddBlogpostComponent {

  model: AddBlogPostModel

  /**
   * 
   */
  constructor(private blogPostService: BlogPostService, private router: Router) {
    this.model = {
      title: '',
      shortDescription: '',
      urlHandle: '',
      content: '',
      featuredImageUrl: '',
      author: '',
      isVisible: true,
      publishedDate: new Date(),
      categories: []
    }
    
  }

  onFormSubmit():void{
    console.log(this.model);
    this.blogPostService.createBlogPost(this.model)
      .subscribe({
        next: (response) => {
            console.log("Add BlogPost Successfull!");
            this.router.navigateByUrl('/admin/blogposts')
        },
        error: (err) => {
          console.error(err);
        }
      });
  }
}
