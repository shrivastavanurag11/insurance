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
export class user {
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
  