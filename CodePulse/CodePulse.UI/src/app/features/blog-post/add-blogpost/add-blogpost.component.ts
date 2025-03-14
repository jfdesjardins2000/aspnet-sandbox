import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { AddBlogPostModel } from '../models/add-blog-post.model';
import { DatePipe } from '@angular/common';

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
  constructor() {
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
  }
}
