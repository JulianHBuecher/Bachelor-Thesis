### Stage 1 Build
FROM node:latest as build
WORKDIR /usr/src/app
COPY package.json package-lock.json ./
RUN npm install

ENV PATH="./node_modules/.bin:$PATH"

COPY . .
RUN ng build --prod --base-href="/"

### Stage 2 Copy Build Files into the container
FROM nginx:alpine
COPY nginx.conf /etc/nginx/nginx.conf
COPY --from=build /usr/src/app/dist/webapp /usr/share/nginx/html
WORKDIR /usr/share/nginx/html
RUN ln -s assets/site-logo.png site-logo.png
