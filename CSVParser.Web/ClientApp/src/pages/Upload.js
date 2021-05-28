import React, { useRef } from 'react';
import { useHistory } from 'react-router-dom';
import axios from 'axios';

const toBase64 = file => new Promise((resolve, reject) => {
    const reader = new FileReader();
    reader.readAsDataURL(file);
    reader.onload = () => resolve(reader.result);
    reader.onerror = error => reject(error);
});

export default function Upload() {

    const fileInputRef = useRef(null);
    const history = useHistory();

    const onUpload = async () => {
        const file = fileInputRef.current.files[0];
        const base64 = await toBase64(file);
        await axios.post('/api/csv/uploadCsv', { base64File: base64 });       
        history.push("/");               
    }
    return (
        <div className="d-flex vh-100" style={{ margintop:"70px"}}>
            <div className="d-flex w-100 justify-content-center align-self-center">
                <div className="row">
                    <div className="col-md-10">
                        <input ref={fileInputRef} type="file" className="form-control-lg" />
                    </div>
                    <div className="col-md-2">
                        <button className="btn btn-primary btn-lg" onClick={onUpload}>Upload</button>
                    </div>
                </div>
            </div>
        </div>
    )
}
