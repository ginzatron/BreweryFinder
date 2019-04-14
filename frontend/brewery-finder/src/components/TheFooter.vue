<template>
    <footer>
        <p>In 2019 we set out to make a brewery finder. We ran on high hops and dreams and found ourselves for foam home. 
          We got the Okeg from our brew/scrum master and long hours and not enough drinking later, we're tapped. 
        </p>
        <div class='social-media'>
          <a href="#" class="fa fa-instagram"></a>  
          <a href="#" class="fa fa-twitter"></a>  
          <a href="#" class="fa fa-reddit"></a>  
          <a href="#" class="fa fa-youtube"></a>  
        </div>
    </footer>
</template>

<script>
export default {

}
</script>

<style scoped>
img {
    position: absolute;
    left: 0%;
    top: 150%;
    width: 100%;
}

.social-media {
  display:flex;
  justify-content: flex-end;
}

.fa {
  padding: 20px;
  font-size: 30px;
  width: 50px;
  text-align: center;
  text-decoration: none;
  height: 15%;
  margin-top: 65px;
}

.fa:hover {
  opacity: 0.7;
}

.fa-instagram {
  background: #125688;
  color: white;
}

.fa-twitter {
  background: #55ACEE;
  color: white;
}

.fa-reddit {
  background: #ff5700;
  color: white;
}

.fa-youtube {
  background: #bb0000;
  color: white;
}

footer {
  display: flex;
  background-color: var(--burgundy);
  color: var(--lightGrey);
  border-top: 3px solid #777;
  height: 300px;
  font-family: archivo;
  justify-content: space-around;
}
p {
  width: 25%;
  line-height: 2rem;
  margin-top: 65px;
}
</style>
