import { Component } from '@angular/core';
//import { RouterLink } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { AddCategoryRequestModel } from '../models/add-category-request.model';
import { CategoryService } from '../services/category.service';
import { error } from 'console';

@Component({
  selector: 'app-add-category',
  standalone: true,
  imports: [FormsModule],
  templateUrl: './add-category.component.html',
  styleUrl: './add-category.component.css'
})
export class AddCategoryComponent {
  // model: {
  //   name: string;
  //   urlHandle: string;
  // } = {
  //   name: '',
  //   urlHandle: ''
  // };
  model: AddCategoryRequestModel;

  constructor(private categoryService: CategoryService) {
    this.model = 
    {
        name: '',
        urlHandle: ''
      };   
  }

  onFormSubmit() {
    // Votre logique de soumission ici
    console.log(`model.name: ${this.model.name}`);
    console.log(this.model);

    this.categoryService.addCategory(this.model)
    .subscribe({
      next: (response) => {
          console.log("Add Category Successfull!");
      },
      error: (err) => {
        console.error(err);
      }
    });
  }
}
