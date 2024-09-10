import { HttpClient, HttpHeaders, HttpResponse } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { consumerBeforeComputation } from "@angular/core/primitives/signals";
import { observableToBeFn } from "rxjs/internal/testing/TestScheduler";
import { DataTransferModel, GroupClaimDetail, Policy, PolicySold, bar, claims, filterOption, moredetail,  salesAnalysis,  user } from "./models";
import { Observable } from "rxjs";
import { response } from "express";

@Injectable({providedIn:'root'})
export class HttpCommunicator
{
    basepath:string=' https://localhost:7112';
    

    constructor(private client:HttpClient){}

    userDetails()
    {
        const token = sessionStorage.getItem('jwttoken');
        const headers = new HttpHeaders().set('Authorization', `Bearer ${token}`);
        /////
        let path = `${this.basepath}/useredetail`;
        var response = this.client.get<user>(path,{observe:'response', headers:headers});
        return response;
    }

    Login(username:string , password:string):Observable<HttpResponse<DataTransferModel>>{
        let path = `${this.basepath}/login`;
        let cred={userName:username,password:password};
        var response = this.client.post<DataTransferModel>(path,cred,{observe:'response'});
        return response;
    }

    Registration(newUser:user)
    {
        let path = `${this.basepath}/registration`;
        var response = this.client.post<user>(path,newUser,{observe:'response', responseType: 'text' as 'json' });
        return response;
    }
    // getEmployees():Observable<HttpResponse<Emp[]>>{
    //     let path=`${this.basePath}/emps`;
    //     var response=this.client.get<Emp[]>(path,{observe:'response'});
    //     return response;
    // }
    checkExistingUser(userName:string)
    {
        let path = `${this.basepath}/checkExistingUser/${userName}`;
        var response = this.client.post<string>(path,{observe:'response'});
        return response;
    }

    //=-------fetching policies -----
    customerHome()
    {
        const token = sessionStorage.getItem('jwttoken');
        const headers = new HttpHeaders().set('Authorization', `Bearer ${token}`);
        /////
        let path = `${this.basepath}/customerHome`;
        var response = this.client.get<Policy[]>(path,{observe:'response', headers:headers});
        return response;
    }

    nextCustomerPage()
    {
        const token = sessionStorage.getItem('jwttoken');
        const headers = new HttpHeaders().set('Authorization', `Bearer ${token}`);
        let path = `${this.basepath}/nextCustomerHome`;
        var response = this.client.get<Policy[]>(path,{observe:'response', headers:headers});
        return response;
    }

    buyPolicy(policyId:number){
        const token = sessionStorage.getItem('jwttoken');
        const headers = new HttpHeaders().set('Authorization', `Bearer ${token}`);
        /////
        let path = `${this.basepath}/buyPolicy/${policyId}`;
        var response = this.client.get<string>(path,{headers:headers,observe:'response',responseType:'text' as 'json' });
        return response;
    }

    myPolicies(){
        const token = sessionStorage.getItem('jwttoken');
        const headers = new HttpHeaders().set('Authorization', `Bearer ${token}`);
        let path = `${this.basepath}/myPolicies`;
        var response = this.client.get<PolicySold[]>(path,{headers:headers,observe:'response' });
        return response;
    }

    claims(){
        const token = sessionStorage.getItem('jwttoken');
        const headers = new HttpHeaders().set('Authorization', `Bearer ${token}`);
        let path = `${this.basepath}/myClaims`;
        var response = this.client.get<GroupClaimDetail[]>(path,{headers:headers,observe:'response' });
        return response;
    }

    newClaim(id:number , claimamount:number , remaining:number)
    {
        const token = sessionStorage.getItem('jwttoken');
        const headers = new HttpHeaders().set('Authorization', `Bearer ${token}`);
        let path = `${this.basepath}/newClaim/${id}/${claimamount}/${remaining}`;
        var response = this.client.get<string>(path,{headers:headers,observe:'response', responseType:'text' as 'json' });
        return response;
    }

    claimDetails(id:number){

        const token = sessionStorage.getItem('jwttoken');
        const headers = new HttpHeaders().set('Authorization', `Bearer ${token}`);
        let path = `${this.basepath}/claimDetails/${id}`;
        var response = this.client.get<moredetail[]>(path,{headers:headers,observe:'response'});
        return response;
    }

    updateProfile(updated:user){
        const token = sessionStorage.getItem('jwttoken');
        const headers = new HttpHeaders().set('Authorization', `Bearer ${token}`);
        let path = `${this.basepath}/updateuser`;
        var response = this.client.post<string>(path,updated,{headers:headers,observe:'response',responseType:'text' as 'json' });
        return response;
    }

    ///// admin services ////
    getUsers(){
        const token = sessionStorage.getItem('jwttoken');
        const headers = new HttpHeaders().set('Authorization', `Bearer ${token}`);
        /////
        let path = `${this.basepath}/UserList`;
        var response = this.client.get<user[]>(path,{observe:'response', headers:headers});
        return response;
    }

    getNextUser(){
        const token = sessionStorage.getItem('jwttoken');
        const headers = new HttpHeaders().set('Authorization', `Bearer ${token}`);
        /////
        let path = `${this.basepath}/NextUserList`;
        var response = this.client.get<user[]>(path,{observe:'response', headers:headers});
        return response;
    }

    searchUser(username: string): Observable<HttpResponse<user>> {
        const token = sessionStorage.getItem('jwttoken');
        const headers = new HttpHeaders().set('Authorization', `Bearer ${token}`);
        /////
        let path = `${this.basepath}/User/${username}`;
        var response = this.client.get<user>(path,{observe:'response', headers:headers});
        return response;
      }

    deleteUser(username: string)
    {
        const token = sessionStorage.getItem('jwttoken');
        const headers = new HttpHeaders().set('Authorization', `Bearer ${token}`);

        let path = `${this.basepath}/Userdelete`;
        var response = this.client.delete<string>(path,{observe:'response', headers:headers, responseType:'text' as 'json'});
        return response;
    }


    updateUser(updated: user){
        const token = sessionStorage.getItem('jwttoken');
        const headers = new HttpHeaders().set('Authorization', `Bearer ${token}`);

        let path = `${this.basepath}/Userupdate`;
        var response = this.client.post<string>(path,updated, {observe:'response', headers:headers, responseType:'text' as 'json'});
        return response;
    }

    addNewPolicy(p:Policy)
    {
        const token = sessionStorage.getItem('jwttoken');
        const headers = new HttpHeaders().set('Authorization', `Bearer ${token}`);

        let path = `${this.basepath}/addNewPolicy`;
        var response = this.client.post<string>(path,p,{observe:'response', headers:headers, responseType:'text' as 'json'});
        return response;
    }

    salesData(){
        const token = sessionStorage.getItem('jwttoken');
        const headers = new HttpHeaders().set('Authorization', `Bearer ${token}`);

        let path = `${this.basepath}/analysis`;
        var response = this.client.get<salesAnalysis[]>(path,{observe:'response', headers:headers});
        return response;
    }

    filterData(criteria:filterOption){
        const token = sessionStorage.getItem('jwttoken');
        const headers = new HttpHeaders().set('Authorization', `Bearer ${token}`);

        let path = `${this.basepath}/filter`;
        var response = this.client.post<salesAnalysis[]>(path,criteria,{observe:'response', headers:headers});
        return response;

    }

    fetchData(searchYear:string) {

        const token = sessionStorage.getItem('jwttoken');
        const headers = new HttpHeaders().set('Authorization', `Bearer ${token}`);
    
        let path = `https://localhost:7112/bar/${searchYear}`;
        var response = this.client.get<bar[]>(path,{observe:'response', headers:headers});
        return response;
      }
}