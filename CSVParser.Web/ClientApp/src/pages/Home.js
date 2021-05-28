import React, { useState, useEffect } from 'react';
import axios from 'axios';

export default function Home() {
    const [ppl, setPpl] = useState([]);
    useEffect(() => {
        const getPpl = async () => {
            const { data } = await axios.get("/api/people/getPpl");
            setPpl(data);
        }
        getPpl();
    }, [])
    const onDelete = () => {
        axios.post("/api/people/deletePpl");
        setPpl([]);
    }
    return (
        <>
            <div className="row">
                <div className="col-md-6 offset-md-3 mt-5">
                    <button className="btn btn-danger btn-lg btn-block" onClick={onDelete}>Delete All</button>
                </div>
            </div>
            <table className="table table-hover table-striped table-bordered mt-5">
                <thead>
                    <tr>
                        <th>Id</th>
                        <th>First Name</th>
                        <th>Last Name</th>
                        <th>Age</th>
                        <th>Address</th>
                        <th>Email</th>
                    </tr>
                </thead>
                <tbody>
                    {ppl.map(p => {
                        const { id, firstName, lastName, age, email, address } = p;
                        return (
                            <tr>
                                <td>{id}</td>
                                <td>{firstName}</td>
                                <td>{lastName}</td>
                                <td>{age}</td>
                                <td>{address}</td>
                                <td>{email}</td>
                            </tr>
                        )
                    })}
                </tbody>
            </table>
        </>

    )
}
