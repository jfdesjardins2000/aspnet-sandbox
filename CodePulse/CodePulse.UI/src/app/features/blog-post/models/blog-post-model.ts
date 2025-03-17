import { CategoryModel } from "../../category/models/category.model";

export interface BlogPostModel {
    id: string;
    title: string;
    shortDescription: string;
    content: string;
    featuredImageUrl: string;
    urlHandle: string;
    author: string;
    publishedDate: Date;
    isVisible: boolean;  
    categories: CategoryModel[];  
  }
  