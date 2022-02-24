import { Component, OnDestroy, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { forkJoin, Subscription } from 'rxjs';
import { EnumData, Product } from 'src/app/entities/entities';
import { CommonService, ProductService } from 'src/app/services/services';

@Component({
  selector: 'app-product-details',
  templateUrl: './product-details.component.html',
  styleUrls: ['./product-details.component.css']
})
export class ProductDetailsComponent implements OnInit, OnDestroy {

  subscriptions$ : Subscription[] = [];
  product : Product = { productType:0 };
  enumData: EnumData[] = [];

  constructor(
    private productService : ProductService,
    private commonService : CommonService,
    private route : ActivatedRoute
  ) { }

  ngOnInit(): void {
    this.subscriptions$.push(
      this.route.params.subscribe(
        (params) => {
          let productId = params['id'];
          if (productId) {
            console.log('productId found', productId);
            this.product.productId = Number(productId);
            if (this.product.productId > 0) {
              this.loadData();
            } else {
              this.loadEnum();
            }
          }
        }
      ));
  }

  ngOnDestroy(): void {
    this.subscriptions$.forEach((subs) => {
      subs.unsubscribe();
    });
  }

  loadData() {
    this.subscriptions$.push(
      forkJoin([
        this.productService.GetProduct<Product>(this.product.productId),
        this.commonService.GetProductTypes<EnumData[]>()
      ]).subscribe({
          next: (value: [product: Product, enumData: EnumData[]]) => {
            this.product = value[0];
            this.enumData = value[1];
          }, error : (error) => {
            alert('Caught an error. Please see logs.');
            console.log('loadData', error);
          }}));
  }

  loadEnum() {
    this.subscriptions$.push(
      this.commonService
          .GetProductTypes<EnumData[]>()
          .subscribe({
            next: (enumData: EnumData[]) => {
              this.enumData = enumData;
            }, 
            error : (error) => {
              alert('Caught an error. Please see logs.');
              console.log('loadEnum', error);
            }}));
  }

  saveData() {
    if (confirm("Save record?")) {
      if (this.product.productId == 0) {
        this.product.productId = undefined;
        this.subscriptions$.push(
          this.productService
              .CreateProduct<Product>(JSON.stringify(this.product))
              .subscribe({
                next: (product: Product) => {
                  this.product = product;
                  alert('Successfully saved!');
                }, 
                error : (error) => {
                  alert('Caught an error. Please see logs.');
                  console.log('loadData', error);   
              }}));
      } else {
        this.subscriptions$.push(
          this.productService
              .UpdateProduct<Product>(JSON.stringify(this.product))
              .subscribe({
                next: (product: Product) => {
                  this.product = product;
                  alert('Successfully saved!');
                }, 
                error : (error) => {
                  alert('Caught an error. Please see logs.');
                  console.log('loadData', error);   
              }}));
      }
    }
  }
  
  onTypeChange() {
    if (this.product.productType==1) {
      this.product.interestRate = 0;
    }
  }
}
