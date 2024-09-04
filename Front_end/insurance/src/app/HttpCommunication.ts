import { HttpClient, HttpResponse } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { consumerBeforeComputation } from "@angular/core/primitives/signals";
import { observableToBeFn } from "rxjs/internal/testing/TestScheduler";
import { DataTransferModel } from "./models";
import { Observable } from "rxjs";

@Injectable({providedIn:'root'})
export class HttpCommunicator
{
    basepath:string=' https://localhost:7112';

    constructor(private client:HttpClient){}

    Login(username:string , password:string):Observable<HttpResponse<DataTransferModel>>{
        let path = `${this.basepath}/login`;
        let cred={userName:username,password:password};
        var response = this.client.post<DataTransferModel>(path,cred,{observe:'response'});
        return response;
    }
    // getEmployees():Observable<HttpResponse<Emp[]>>{
    //     let path=`${this.basePath}/emps`;
    //     var response=this.client.get<Emp[]>(path,{observe:'response'});
    //     return response;
    // }
}