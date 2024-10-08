import { Injectable } from "@angular/core";
import { Router } from "@angular/router";

export class SecurityTokenModel
{
    constructor(public role:string , public jwttoken:string ){}
}


export class DataTransferModel
{
    constructor(public data?:SecurityTokenModel , public message?:string , public success?:boolean ){}
}

export class UserCredentials{
    constructor(public userName?:string,public password?:string){}
}


// ----------------------------   models for costumer service------------------
export class user 
{
  public userID!: number;
  public userName!: string;
  public password!: string;
  public userType!: string; // Admin or Customer
  public firstName!: string;
  public lastName!: string;
  public age?: number; // Optional
  public gender?: string; // Optional this shoul be 'M' or 'F'
  public email!: string;
  public contactNo!: string;
  public address!: string;
  public customerImage?: any; // Optional

  constructor() {}
}

export class PolicySold {
  purchaseId!: number;
  userId?: number;
  policyId?: number;
  soldDate!: Date;
  amount!: number;
  duration?: number;
}

export class claims {
  userName?: string;
  firstName?: string;
  policyId?: number;
  purchasedOn?: Date;
  amount?: number;
  claimedAmount?: number;
  remainingAmount?: number;
  claimDate?: Date;
}

export class moredetail
{
  date?: Date;
  amount?: number;
  remainingamount?: number;
}

export class GroupClaimDetail 
{
  purchaseId!:number;
  policyId!:number;
  policyName!: string;
  policyType!: string;
  insuredAmount!: number;
  numberOFClaims?: number;
  totalClaimedAmount?: number;
  lastClaimDate?: Date;
  remainingAmount: number=0;
  show:boolean=false;
  showMore:boolean=false;
  newClaim!:number;
  moredetailed!:moredetail[];
}


  


  //---  model for policy-----
export class Policy {
  policyId!: number;
  policyType!: string;
  policyName!: string;
  insuranceAmount!: number;
  policyValidity!: number;
  policyDescription!: string;
  available?: string;

  constructor() {}
}
  

//  admin models //
export class salesAnalysis 
{
  policyID?: number;
  policyName?: string;
  userName?: string;
  amount?: number;
  policyType?: string;
  soldOn!: Date;
}



export class filterOption
{
      policyId?: number = undefined;
      policyType?: string = undefined;
      policyName?: string = undefined;
      userName?: string = undefined;
      customerName?: string = undefined;
      startDate?: Date = undefined;
      endDate?: Date = undefined;
}

export class bar{
  
    x?: number;
    y?: number;
}

  //--------

@Injectable({providedIn:'root'})
export class adminGuard
{
    constructor(private router:Router){}
    canActivate():boolean{
        var role= sessionStorage.getItem('role');
        if(role?.toLocaleLowerCase()!='admin'){
            this.router.navigate(["login"]);
            return false;
        }
        return true;

    }    




    
}
