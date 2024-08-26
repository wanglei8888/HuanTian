<template>
  <a-modal
    :title="title"
    :width="1000"
    :visible="visible"
    :confirmLoading="confirmLoading"
    @ok="handleOk"
    @cancel="handleCancel">
    <a-spin :spinning="spinningLoading">
      <a-form-model ref="form" :model="form" :label-col="labelCol" :wrapper-col="wrapperCol" :rules="rules">
        <a-row :gutter="gutter">
          <a-col :md="mdSize" :sm="smSize">
            <a-form-model-item label="账号" prop="userName" hasFeedback>
              <a-input v-model="form.userName" />
            </a-form-model-item>
          </a-col>
          <a-col :md="mdSize" :sm="smSize">
            <a-form-model-item label="密码" prop="password" hasFeedback>
              <a-input-password v-if="formType === 'edit'" :disabled="true"  v-model="form.password" />
              <a-input-password v-else  v-model="form.password" />
            </a-form-model-item>
          </a-col>
        </a-row>
        <a-row :gutter="gutter">
          <a-col :md="mdSize" :sm="smSize">
            <a-form-model-item label="性别" prop="gender" hasFeedback>
              <a-select style="width: 100%" v-model="form.gender" >
                <a-select-option v-for="(item, index) in genderList" :key="index" :value="parseInt(item.value)">
                  {{ item.name }}
                </a-select-option>
              </a-select>
            </a-form-model-item>
          </a-col>
          <a-col :md="mdSize" :sm="smSize">
            <a-form-model-item label="邮箱" prop="email" hasFeedback>
              <a-input  v-model="form.email" />
            </a-form-model-item>
          </a-col>
        </a-row>
        <a-row :gutter="gutter">
          <a-col :md="mdSize" :sm="smSize">
            <a-form-model-item label="用户名称" prop="name" hasFeedback>
              <a-input v-model="form.name" />
            </a-form-model-item>
          </a-col>
          <a-col :md="mdSize" :sm="smSize">
            <a-form-model-item label="电话号码" prop="telephone" hasFeedback>
              <a-input v-model="form.telephone" />
            </a-form-model-item>
          </a-col>
        </a-row>
        <a-row :gutter="gutter">
          <a-col :md="mdSize" :sm="smSize">
            <a-form-model-item label="所属部门" prop="deptId" hasFeedback>
              <a-select style="width: 100%" v-model="form.deptId">
                <a-select-option v-for="(item, index) in deptData" :key="index" :value="item.id">
                  {{ item.name }}
                </a-select-option>
              </a-select>
            </a-form-model-item>
          </a-col>
          <a-col :md="mdSize" :sm="smSize">
            <a-form-model-item label="默认语言" prop="language" hasFeedback>
              <a-select style="width: 100%" v-model="form.language">
                <a-select-option v-for="(item, index) in languageData" :key="index" :value="item.value">
                  {{ item.name }}
                </a-select-option>
              </a-select>
            </a-form-model-item>
          </a-col>
        </a-row>
        <a-row :gutter="gutter">
          <a-col :md="mdSize" :sm="smSize">
            <a-form-model-item label="用户头像" prop="avatar" hasFeedback>
              <div class="clearfix">
                <a-upload
                  list-type="picture-card"
                  :file-list="fileList"
                  :before-upload="beforeUpload"
                  @preview="handlePreview"
                  @change="handleChange"
                  accept="image/jpeg,image/jpg,image/png">
                  <div v-if="fileList.length < 1">
                    <a-icon type="plus" />
                    <div class="ant-upload-text">
                      Upload
                    </div>
                  </div>
                </a-upload>
                <a-modal :visible="previewVisible" :footer="null" @cancel="ImghandleCancel">
                  <img alt="example" style="width: 100%" :src="previewImage" />
                </a-modal>
              </div>
            </a-form-model-item>
          </a-col>
          <a-col :md="mdSize" :sm="smSize">
            <a-form-model-item label="是否启用">
              <a-switch checkedChildren="是" v-model="form.enable" unCheckedChildren="否" />
            </a-form-model-item>
          </a-col>
        </a-row>
      </a-form-model>
    </a-spin>
  </a-modal>
</template>

<script>
import { sysDict } from '@/api/system'
import md5 from 'md5'
function createForm () {
  return {
    language: 'zh_CN',
    enable: true,
    gender: 0
  }
}
export default {
  name: 'RoleModal',
  data () {
    return {
      labelCol: {
        md: { span: 6 },
        sm: { span: 4 }
      },
      wrapperCol: {
        md: { span: 14 },
        sm: { span: 20 }
      },
      gutter: 24,
      mdSize: 12,
      smSize: 24,
      previewVisible: false,
      formType: 'add',
      previewImage: '',
      languageData: [],
      deptData: [],
      genderList:[],
      fileList: [],
      title: '添加角色',
      spinningLoading: false,
      visible: false,
      confirmLoading: false,
      rules: {
        userName: [{ required: true, min: 1, message: '请输入账号！' }],
        userName: [{ required: true, min: 1, message: '请输入账号！' }],
        password: [{ required: true, min: 1, message: '请输入密码！' }],
        telephone: [{ required: true, validator: this.phoneValidator}],
        name: [{ required: true, min: 1, message: '请输入用户名称！' }],
        deptId: [{ required: true, message: '请选择所属部门！' }],
        language: [{ required: true, message: '请选择默认语言！' }],
        email: [{ required: true, validator: this.emailValidator }],
        avatar: [{required: true, validator: this.avatarValidator, trigger: ['blur']}]
      },
      form: {}
    }
  },
  methods: {
    // 打开页面初始化
    detail (value) {
      // this.$store.getters.userInfo.avatar
      this.visible = true
      this.confirmLoading = false
      if (this.$refs.form) {
        this.$refs.form.clearValidate()
      }
      this.dromdownList()
      if (value) {
        this.form = JSON.parse(JSON.stringify(value))
        // JSON.parse(JSON.stringify())
        this.formType = 'edit'
        this.title = '编辑用户'
        this.fileList = [{
          uid: '-1',
          name: this.form.avatar.substring(this.form.avatar.lastIndexOf('\\') + 1),
          status: 'done',
          url: this.form.avatar,
          thumbUrl: this.form.avatar
        }]
      } else {
        this.fileList = []
        this.title = '新建用户'
        this.formType = 'add'
        this.form = createForm()
      }
    },
    close () {
      this.$emit('close')
      this.visible = false
    },
    async handleOk () {
      this.$refs.form.validate(async valid => {
        if (valid) {
          this.form.password = md5(this.form.password)
          // 如果存在修改文件
          if (this.fileList.length > 0 && this.fileList[0].originFileObj) {
            const formData = new FormData()
            formData.append('filePath', 'userAvatar')
            if (this.formType === 'edit') {
              formData.append('add', false)
            }
            formData.append('file', this.fileList[0].originFileObj)
            await this.$http.post('/sysFile/upload', formData).then(res => {
              // 上传保存信息
              this.form.avatar = res.result.filePath
            })
          }
          this.confirmLoading = true
          // 修改数据
          if (this.formType === 'edit') {
            await this.$http.put('/sysUser', this.form).then(res => {
              if (res.code === 200) {
                this.$message.success('保存成功')
                this.$emit('ok')
                this.visible = false
              }
            }).finally(() => {
              this.confirmLoading = false
            })
          } else {
            // 保存数据
            // 先上传文件
            await this.$http.post('/sysUser', this.form).then(res => {
              if (res.code === 200) {
                this.$message.success('保存成功')
                this.$emit('ok')
                this.visible = false
              }
            }).finally(() => {
              this.confirmLoading = false
            })
          }
        }
      })
    },
    handleCancel () {
      this.close()
    },
    avatarValidator (rule, value, callback) {
      // if (this.fileList.length === 0) {
      //   return callback('请上传用户头像！')
      // } else {
      return true
      // }
    },
    phoneValidator(rule,value,callback){
        // 如果为空值，就抛出错误
        if(!value){
          callback(new Error("手机号码不能为空"));
        }else{
          // 正则判断手机号格式的格式，正则判断失败抛出错误，否则直接callback()
          if(!/^1[2-9]\d{9}$/.test(value)){
            callback(new Error("手机号格式不正确!"));
          }else{
            callback();
          }
        }
     },
     /**
      * 邮箱验证规则
      * @param rule 
      * @param value 值
      * @param callback 
      */
    emailValidator(rule,value,callback){
      var email = value;
			var reg = /^([a-zA-Z]|[0-9])(\w|\-)+@[a-zA-Z0-9]+\.([a-zA-Z]{2,4})$/;
      if(!value){
        callback(new Error("邮箱不能为空"));
      }
			if(reg.test(email)){
				callback();
			}else{
				callback(new Error("邮箱格式不正确!"));
			}
    },
    dromdownList () {
      sysDict({ code: 'languageType' }).then((res) => {
        if (res.code === 200) {
          this.languageData = res.result
        }
      })
      sysDict({ code: 'userGender' }).then((res) => {
        if (res.code === 200) {
          this.genderList = res.result
        }
      })
      this.$http.get('/sysDept').then((res) => {
        if (res.code === 200) {
          this.deptData = res.result
        }
      })
    },
    beforeUpload (file) {
      this.$refs.form.clearValidate('avatar')
      this.$refs.form.clearValidate('name')
      return false
    },
    ImghandleCancel () {
      this.previewVisible = false
    },
    async handlePreview (file) {
      if (!file.url && !file.preview) {
        file.preview = await getBase64(file.originFileObj)
      }
      this.previewImage = file.url || file.preview
      this.previewVisible = true
    },
    handleChange ({ fileList }) {
      fileList.forEach((file) => {
        const isLt50M = file.size / 1024 / 1024 < 5
        const isJpgOrPng =
          file.type === 'image/jpeg' || file.type === 'image/png'
        if (!isLt50M) {
          fileList.splice(file, 1)
          this.$message.error('上传头像图片大小不能超过 5MB!')
          return false
        } else if (!isJpgOrPng) {
          fileList.splice(file, 1)
          this.$message.error('上传头像图片只能是 JPG/PNG 格式!')
          return false
        }
      })
      this.fileList = fileList
    }
  }
}
function getBase64 (file) {
  return new Promise((resolve, reject) => {
    const reader = new FileReader()
    reader.readAsDataURL(file)
    reader.onload = () => resolve(reader.result)
    reader.onerror = error => reject(error)
  })
}
</script>

<style scoped></style>
<style>
/* you can make up upload button and sample style by using stylesheets */
.ant-upload-select-picture-card i {
  font-size: 32px;
  color: #999;
}

.ant-upload-select-picture-card .ant-upload-text {
  margin-top: 8px;
  color: #666;
}
</style>
