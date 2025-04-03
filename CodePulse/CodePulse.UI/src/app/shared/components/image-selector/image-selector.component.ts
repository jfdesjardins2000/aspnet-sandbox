import { Component, OnInit, OnDestroy, ViewChild } from '@angular/core';
import { Observable, Subscription } from 'rxjs';
import { BlogImageModel } from '../../models/blog-image.model';
import { NgForm } from "@angular/forms";
import { FormsModule } from '@angular/forms'; 
import { ImageService } from '../../services/image.service';
import { AsyncPipe } from '@angular/common';

@Component({
  selector: 'app-image-selector',
  standalone: true,
  imports: [FormsModule, AsyncPipe],
  templateUrl: './image-selector.component.html',
  styleUrl: './image-selector.component.css'
})
export class ImageSelectorComponent implements OnInit, OnDestroy {
  
  file?: File;
  fileName: string = '';
  title: string = '';
  images$?: Observable<BlogImageModel[]>;

  @ViewChild('form', { static: false}) imageUploadForm?: NgForm;

  private uploadImageSubscription?: Subscription;

  constructor(private imageService: ImageService) {
  }

  ngOnInit(): void {
    this.getImages();
  }

  onFileUploadChange(event: Event): void {
    const element = event.currentTarget as HTMLInputElement;
    this.file = element.files?.[0];
  }

  uploadImage():void {
    if (this.file && this.fileName !== '' && this.title !== '') {
      this.uploadImageSubscription = this.imageService.uploadImage(this.file, this.fileName, this.title)
      .subscribe({
        next: (response: BlogImageModel) => {
          console.log('Image uploaded successfully:', response);
          this.imageUploadForm?.resetForm();
          this.getImages();
        },
      });
    }
  }

  selectImage(image: BlogImageModel): void {
    this.imageService.selectImage(image);
  }
    

  private getImages() {
    this.images$ = this.imageService.getAllImages();
  }

  ngOnDestroy(): void {
    console.log('uploadImageSubscription.unsubscribe');
    this.uploadImageSubscription?.unsubscribe();
  }

}

