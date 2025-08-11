const form =document.getElementById("feedbackForm");
const feedbackList = document.getElementById("feedbackList");
const apiUrl = "http://localhost:5000/api/feedback";

form.addEventListener("submit", async(e) => {
    e.preventDefault();

    const feedback = {
        guestName: document.getElementById("guestName").value,
        role: document.getElementById("role").value,
        message: document.getElementById("message").value,
    };

    try{
        const res = await fetch(apiUrl, {
            method: "POST",
            headers: {"Content-Type": "application/json"},
            body: JSON.stringify(feedback),
        });

        if(res.ok){
            form.reset();
            loadFeedback(); //Refresh List
        }else{
            alert("Failed to submit feedback.");
        }
    }catch(err){
        console.error(err);
    }
});

async function loadFeedback(){
    try{
        const res =await fetch(apiUrl);
        const data = await res.json();

        feedbackList.innerHTML=""; //Clear
        data.reverse().forEach(item=> {
            const card = document.createElement("div");
            card.innerHTML =`
            <p><strong>${item.guestName}</strong> (${item.role})</p>
            <p>${item.message}</p>
            <small>${new Date(item.submittedAt).toLocaleString()}</small>
            <hr/>`;
            feedbackList.appendChild(card);
        });
    } catch(err){
        console.error("Failed to load feedback:", err);
    }
}

loadFeedback(); //Load on page start