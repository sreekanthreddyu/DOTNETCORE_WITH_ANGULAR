import { __decorate } from "tslib";
import { Component } from "@angular/core";
let ProductList = /** @class */ (() => {
    let ProductList = class ProductList {
        constructor() {
            this.products = [{
                    title: "First Product",
                    price: 19.99
                }, {
                    title: "Second Product",
                    price: 9.99
                }, {
                    title: "Third Product",
                    price: 14.99
                }];
        }
    };
    ProductList = __decorate([
        Component({
            selector: "product-list",
            templateUrl: "./productList.component.html",
            styleUrls: []
        })
    ], ProductList);
    return ProductList;
})();
export { ProductList };
//# sourceMappingURL=productlist.component.js.map