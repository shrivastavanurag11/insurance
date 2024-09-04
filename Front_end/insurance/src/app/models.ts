export class SecurityTokenModel
{
    constructor(public role:string , public jwttoken:string ){}
}


export class DataTransferModel
{
    Data!:SecurityTokenModel;
    Message:string='';
    Success:boolean=false;
   // constructor(public Data?:SecurityTokenModel , public Message?:string , public Success?:boolean ){}
}

export class UserCredentials{
    constructor(public userName?:string,public password?:string){}
}

