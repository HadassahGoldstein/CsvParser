import React,{useState} from 'react';

export default function Generate() {
    const [amount,setAmount]=useState(0);
   
    const onGenerate =  () => {       
        window.location = `/api/csv/generateCsv/${amount}`;
}
    return (
        <div className="d-flex vh-100" style={{ margintop:  "70px"}}>
            <div className="d-flex w-100 justify-content-center align-self-center">
                <div className="row">
                    <input type="text" className="form-control-lg" placeholder="Amount"  onChange={(e)=>setAmount(e.target.value)}/>
                </div>
                <div className="row">
                    <div className="col-md-4">
                        <button className="btn btn-primary btn-lg ml-4" disabled={amount==0}onClick={onGenerate}>Generate</button>
                    </div>
                </div>
            </div>
        </div>
    )

}