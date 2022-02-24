import { Component, OnDestroy, OnInit } from '@angular/core';
import { forkJoin, Subscription } from 'rxjs';
import { EnumData, Product } from 'src/app/entities/entities';
import { CommonService, ProductService } from 'src/app/services/services';

@Component({
  selector: 'app-products',
  templateUrl: './products.component.html',
  styleUrls: ['./products.component.css']
})
export class ProductsComponent implements OnInit, OnDestroy {

  subscriptions$ : Subscription[] = [];
  products: Product[] = [];
  enumData: EnumData[] = [];
  colHeaders: string[] = ["Product Id", "Product Name", "Product Type", "Interest Rate", "Establishment Fee", "Actions"];

  constructor(
    private productService : ProductService,
    private commonService : CommonService
  ) { }

  ngOnInit(): void {
    this.loadData();
  }

  ngOnDestroy(): void {
    this.subscriptions$.forEach((subs) => {
      subs.unsubscribe();
    });
  }

  loadData() {
    this.subscriptions$.push(
      forkJoin([
        this.productService.GetProducts<Product[]>(),
        this.commonService.GetProductTypes<EnumData[]>()
      ]).subscribe({
          next: (value: [products: Product[], enumData: EnumData[]]) => {
            this.products = value[0];
            this.enumData = value[1];
          }, error : (error) => {
            alert('Caught an error. Please see logs.');
            console.log('loadData', error);
          }}));
  }

  getProductTypeDescription(id?: number) : string {
    let result = '';
    this.enumData.forEach((item) => {
      if (item.id == id) {
        result = item.description || '';
      }
    });
    return result;
  }

}
